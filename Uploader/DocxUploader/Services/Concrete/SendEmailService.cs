using DocxUploader.Configuration;
using DocxUploader.Models;
using DocxUploader.Services.Abstract;
using Microsoft.Extensions.Options;
//using MailKit.Net.Smtp;
//using MailKit.Security;

//using MimeKit;
using System.Net;
using System.Net.Mail;
namespace DocxUploader.Services.Concrete
{
    public class SendEmailService:ISendEmailService
    {
        public readonly JsonFileMailService _jsonService;
        private readonly MailSettings _mailSettings;
        public SendEmailService(IOptions<MailSettings> mailSettingsOptions, JsonFileMailService jsonService)
        {
            _jsonService = jsonService;        
             _mailSettings = mailSettingsOptions.Value;
        }
        public async Task Send()
        {
            MailModel _model = await _jsonService.GetMailModel();

            // setting user Email to ""
            await _jsonService.ResetUserMail();
            //"Blob Notification"
            // $"<h1>The file is successfully uploaded</h1><br>link:<a href=\"{_model.LinkToFile}\">link text</a>";

            try
            {
                MailAddress? from, to;
                MailAddress.TryCreate(_mailSettings.SenderEmail, out from);
                MailAddress.TryCreate(_model.To, out to);
                MailMessage mc = new MailMessage(from, to);
                mc.Subject = "Blob Notification";
                mc.Body = $"<h1>The file is successfully uploaded</h1><br>link:<a href=\"{_model.LinkToFile}\">link text</a>";
                mc.IsBodyHtml = true;
                mc.Priority = MailPriority.High;         
               
                var smtpClient = new SmtpClient(_mailSettings.Server)
                {
                    Port = _mailSettings.Port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password),                    
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network

                };

                smtpClient.Send(mc);

                //var email = new MimeMessage();
                //email.From.Add(MailboxAddress.Parse(_mailSettings.SenderEmail));
                //email.To.Add(MailboxAddress.Parse(_model.To));
                //email.Subject = "Blob Notification";
                //email.XPriority = XMessagePriority.Highest;
                //email.Priority = MessagePriority.Urgent;
                //email.Importance = MessageImportance.High;               
                //email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"<h1>The file is successfully uploaded</h1><br>link:<a href=\"{_model.LinkToFile}\">link text</a>" };
                //using (var smtp = new SmtpClient())
                //{
                //    smtp.Timeout = 100000;
                //    smtp.Connect(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
                //    smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
                //    smtp.Send(email);
                //    smtp.Disconnect(true);
                //}
                              

            } 
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
