using System.ComponentModel;

namespace DocxUploader.Models
{
    public class DocxFileModel
    {
        public MailModel Sender { get; set; }
        public IFormFile File { get; set; }
    }
}
