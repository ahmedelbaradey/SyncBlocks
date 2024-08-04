using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Shared.DataTransferObjects.Block;
using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.UserObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects.UserObjectPermission;
using MassTransit;
using CrossCuttingLayer;
using GreenPipes;

namespace AzureUploader.Services
{
    /// <summary>
    /// Perform chunked uploads to Azure blob storage
    /// </summary>
    public class BlockBlobUploader : IBlockUploader
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly IBusControl _bus;
        public BlockBlobUploader(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                {
                    h.Username(RabbitMqConsts.UserName);
                    h.Password(RabbitMqConsts.Password);
                });
            });

            _bus.StartAsync();
        }        

        public string AccountName { get => new BlobContainerClient(_connectionString, _containerName).AccountName; }

        public string ContainerName { get => _containerName; }
        public async Task UploadBlobInBlocksAsync(string userName, HttpRequest request, string prefix = null)
        {

            // Get a reference to a new block blob.
            var containerClient = new BlobContainerClient(_connectionString, _containerName);
            await containerClient.CreateIfNotExistsAsync();

            // Specify the block size as 4 MB.
            int blockSize = 4 * 1024 * 1024;
         

            foreach (var file in request.Form.Files)
            {
              
                var message = new QueueMessage();
                message.ParentObject = "root";
                message.UserId = 1;
                message.SharedObjectForCreationDto  = new SharedObjectForCreationDto(
                    Name: file.FileName,
                    Type: "File",
                    RelativePath: "root/",
                    Journals: new List<JournalForCreationDto>(){ new JournalForCreationDto(
                        Timestamp: DateTime.Now,
                        ChangeType: "Create",
                        UserId: 1,
                        DeviceId: 1,
                        Blocks: new List<BlockForCreationDto>()
                        ) },
                    UserObjects: new List<UserObjectForCreationDto>(){  new UserObjectForCreationDto(
                        UserId: 1,
                        UserObjectPermissions:new List<UserObjectPermissionForCreationDto>(){  new UserObjectPermissionForCreationDto(PermissionId: 4) }
                        ) }
                    );
                var blob = GetBlobClient(file.FileName, prefix);
                using (var stream = file.OpenReadStream())
                {
                    long streamSize = stream.Length;
                    try
                    {
                        // The number of bytes read so far.
                        int bytesRead = 0;

                        // The number of bytes left to read and upload.
                        long bytesLeft = streamSize;
                        int blockId = 1;
                        // Loop until all of the bytes in the stream have been uploaded.
                        while (bytesLeft > 0)
                        {
                            int bytesToRead;

                            // Check whether the remaining bytes constitute at least another block.
                            if (bytesLeft >= blockSize)
                            {
                                // Read another whole block.
                                bytesToRead = blockSize;
                            }
                            else
                            {
                                // There's less than a whole block left, so read the rest of it.
                                bytesToRead = (int)bytesLeft;
                            }

                            // Set up a new buffer for writing the block, and read that many bytes into it .
                            byte[] bytesToWrite = new byte[bytesToRead];
                            stream.Read(bytesToWrite, 0, bytesToRead);

                            // Calculate the MD5 hash of the buffer.
                            MD5 md5 = new MD5CryptoServiceProvider();
                            byte[] blockHash = md5.ComputeHash(bytesToWrite);
                            string md5Hash = Convert.ToBase64String(blockHash, 0, 16);

                            // Create a block ID from the block number, and add it to the block ID list.
                            // Note that the blockNumber is a base64 string.
                            var blockNumber = ToBase64(blockId);
                            var currentBlock = new BlockForCreationDto(blockNumber, md5Hash);

                            message.SharedObjectForCreationDto.Journals.FirstOrDefault().Blocks.Add(currentBlock);
                            // Upload the block with the hash.
                            await blob.StageBlockAsync(base64BlockId: blockNumber, content: new MemoryStream(bytesToWrite), transactionalContentHash: blockHash);
                            // Increment and decrement the counters.
                            bytesRead += bytesToRead;
                            bytesLeft -= bytesToRead;
                            blockId++;
                        }
                        // Commit the blocks to a new blob.
                        await CommitAsync(userName, file.FileName);

                        Uri uri = new Uri(RabbitMqConsts.RabbitMqUriNotifyMetaDataService);
                        var endPoint = await _bus.GetSendEndpoint(uri);
                        await endPoint.Send(message);
                    }
                    catch (RequestFailedException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                        throw;
                    }
                }
            }
        }
        private string ToBase64(int id) => Convert.ToBase64String(Encoding.UTF8.GetBytes(id.ToString("d6")));
        public async Task  CommitAsync(string userName, string fileName, string prefix = null, IDictionary<string, string> metadata = null, byte[] hash = null)
        {
            var client = GetBlobClient(fileName, prefix);
            var blockList = (await client.GetBlockListAsync()).Value;
            var blockIds = blockList.UncommittedBlocks.Select(block => block.Name);

            await client.CommitBlockListAsync(blockIds, new BlobHttpHeaders()
            {
                ContentType = GetContentType(),
                ContentHash = hash
            });

            if (metadata != null)
            {
                await client.SetMetadataAsync(metadata);
            }
            string GetContentType()
            {
                var provider = new FileExtensionContentTypeProvider();
                return (provider.TryGetContentType(fileName, out string contentType)) ? contentType : "application/octet-stream";
            }
            
        }

        /// <summary>
        /// added this so I could delete and retry uploads that were canceled or not done right when I was figuring out how this worked
        /// </summary>        
        public async Task ResolveUncommittedAsync()
        {
            var containerClient = new BlobContainerClient(_connectionString, _containerName);
            var results = containerClient.GetBlobsAsync(traits: BlobTraits.None, states: BlobStates.Uncommitted);

            await foreach (var page in results.AsPages())
            {
                foreach (var item in page.Values)
                {
                    var blobClient = new BlockBlobClient(_connectionString, _containerName, item.Name);

                    try
                    {
                        var blockList = await blobClient.GetBlockListAsync();
                        var blockIds = blockList.Value.UncommittedBlocks.Select(block => block.Name);
                        await blobClient.CommitBlockListAsync(blockIds);
                    }
                    catch
                    {
                        // ignore, traits filter doesn't seem to work, so I just try/catch around the invalid blobs
                    }
                }
            }
        }

        public static string BlobName(string prefix, string fileName) => string.Join("/", new string[]
        {
            prefix, fileName
        }.Where(s => !string.IsNullOrEmpty(s)));

        public BlockBlobClient GetBlobClient(string fileName, string prefix = null) => new BlockBlobClient(_connectionString, _containerName, BlobName(prefix, fileName));

    }
}
