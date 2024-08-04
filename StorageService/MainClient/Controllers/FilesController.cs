using AzureUploader.Services;
using MainClient.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainClient.Controllers
{
    public class FilesController : Controller
    {
        private readonly BlockBlobUploader _uploader;
        
        public FilesController(BlockBlobUploader uploader)
        {    
            _uploader = uploader;
         
        }

        [HttpPost]        
        public async Task<IActionResult> Stage()
        {
            await _uploader.UploadBlobInBlocksAsync("default", Request);
            return new OkResult();
        }
        
        //[HttpPost]
        //public async Task<IActionResult> Commit([FromForm]string fileName)
        //{            
        //    await _uploader.CommitAsync("default", fileName);            
        //    return new OkResult();
        //}    
    }
}
