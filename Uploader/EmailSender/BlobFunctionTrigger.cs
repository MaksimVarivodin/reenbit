using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
namespace TriggerToSendMessage
{
    public class BlobFunctionTrigger
    {
        [FunctionName("Function")]
        public void Run([BlobTrigger("testtaskcontainer/{name}", Connection = "ConnectionString")]Stream myBlob, string name, ILogger log)
        {
            //   docxuploaderapp.azurewebsites.net
            // Call Your  API
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");         
            

            HttpClient newClient = new HttpClient();
            // https://localhost:7104/
            HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Post, "https://docxuploaderapp.azurewebsites.net/Send/SendMail");
            // used here request was used for testing 
            //HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7104/Send/SendMail");
            newClient.SendAsync(newRequest);
            log.LogInformation("Called Api");

        }
    }
}
