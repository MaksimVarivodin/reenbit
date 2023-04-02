using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocxUploader.Models;
using DocxUploader.Options;
using DocxUploader.Services.Abstract;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace DocxUploader.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly AzureOptions _azureOptions;

        public FileService(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions.Value;
        }
        public void UploadFileToAzure(DocxFileModel file)
        {
            string fileExtension = Path.GetExtension(file.File.FileName);

            using MemoryStream fileUploadStream = new MemoryStream();   
            file.File.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;
            BlobContainerClient blobContainer = new BlobContainerClient(
                _azureOptions.ConnectionString,
                _azureOptions.Container);
            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainer.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            { 
             
                HttpHeaders = new BlobHttpHeaders 
                { 
                ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                }

            }, cancellationToken:default);
            file.Sender.LinkToFile =  blobContainer.Uri.AbsoluteUri +"/"+uniqueName;
        }
    }
}
