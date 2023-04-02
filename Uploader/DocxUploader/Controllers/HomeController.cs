using DocxUploader.Models;
using DocxUploader.Services;
using DocxUploader.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DocxUploader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileService _fileService;
        private readonly JsonFileMailService _jsonService;

        public HomeController(ILogger<HomeController> logger, IFileService fileService, JsonFileMailService jsonService)
        {
            _logger = logger;
            _fileService = fileService;
            _jsonService = jsonService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDoc(DocxFileModel fileModel)
        {


            // uploading file to azure blob
            if(fileModel.File !=null && fileModel.File.FileName!= null)
                _fileService.UploadFileToAzure(fileModel);

            // saving users mail and lonk to file to send message
            _jsonService.SetUserMail(fileModel.Sender.To, fileModel.Sender);
            return View("Index");

        }
        public IActionResult Index()
        {

            //_jsonService.SetUserMail("");
            _jsonService.ResetUserMail();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}