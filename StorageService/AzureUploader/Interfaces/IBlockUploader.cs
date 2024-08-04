using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureUploader.Services
{
    public interface IBlockUploader
    {
        Task UploadBlobInBlocksAsync(string userName, HttpRequest request, string prefix = null);
        Task CommitAsync(string userName, string fileName, string prefix = null, IDictionary<string, string> metadata = null, byte[] hash = null);
    }
}