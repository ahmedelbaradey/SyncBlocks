using Microsoft.AspNetCore.Mvc.RazorPages;
using AzureUploader.Services;


namespace MainClient.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Uri> ExistingFiles { get; set; }
        private readonly BlockBlobUploader _uploader;

        public IndexModel(MainClient.Interfaces.IFileStorage storage, BlockBlobUploader uploader)
        {
            Storage = storage;
            _uploader = uploader;
        }

        public MainClient.Interfaces.IFileStorage Storage { get; }

        public void OnGet()
        {
            ExistingFiles = Storage.ListContents();


        }



    }
}