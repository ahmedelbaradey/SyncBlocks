using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using MainClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MainClient.Services
{
    public class BlobStorage : IFileStorage
    {
        private readonly string _connectionString;
        private readonly ILoggerManager _logger;

        public BlobStorage(string connectionString, string containerName,ILoggerManager loggerFactory)
        {
            _connectionString = connectionString;
            ContainerName = containerName;
            _logger = loggerFactory;
        }

        public string ContainerName { get; }

        public IEnumerable<Uri> ListContents()
        {
            var container = new BlobContainerClient(_connectionString, ContainerName);
            var result = container.GetBlobs();
            _logger?.LogInfo($"Copied");
            return result.Select(item => new Uri(container.Uri + "/" + item.Name));
        }


    }
}
