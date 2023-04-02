using DocxUploader.Models;

namespace DocxUploader.Services.Abstract
{
    public interface IFileService
    {
        void UploadFileToAzure(DocxFileModel File);
    }
}
