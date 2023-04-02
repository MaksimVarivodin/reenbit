
using DocxUploader.Models;
using DocxUploader.Services;
using DocxUploader.Services.Abstract;
using DocxUploader.Services.Concrete;
using Microsoft.AspNetCore.Mvc;


namespace DocxUploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendController : ControllerBase
    {
        private readonly ISendEmailService _mailService;
        public SendController(ISendEmailService _MailService)
        {
            
            _mailService = _MailService;

        }
        [HttpPost]
        [Route("SendMail")]
        public async Task SendMail()
        {
            await _mailService.Send();
     
        }
       
    }
}
