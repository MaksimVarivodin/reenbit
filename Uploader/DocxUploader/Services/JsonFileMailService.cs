using DocxUploader.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocxUploader.Services
{
    public class JsonFileMailService
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public JsonFileMailService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        private string JsonFileName 
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "mail.json"); }
        }

        public async Task<MailModel> GetMailModel()
        {
            using (FileStream fs = new FileStream(JsonFileName, FileMode.OpenOrCreate))
            {
                return await JsonSerializer.DeserializeAsync<MailModel>(fs);
                
            }            
        }
        private async Task SetMailModelAsync(MailModel model)
        {
            using (FileStream fs = new FileStream(JsonFileName, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<MailModel>(fs, model);
            }
        }
        public async Task SetUserMail(string ToUser, MailModel mail) {
            mail.To = ToUser;
            await SetMailModelAsync(mail);
        }
        public async Task ResetUserMail()
        {
            //var sender = GetMailModel();
            var sender = new MailModel();
            sender.To = null;
            sender.LinkToFile = null;
            await SetMailModelAsync(sender);
        }
    }
}
