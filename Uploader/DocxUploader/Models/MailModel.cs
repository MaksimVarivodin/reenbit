
using System.Text.Json;
using System.Text.Json.Serialization;
using DocxUploader.Configuration;

namespace DocxUploader.Models
{
    public class MailModel
    {
        [JsonPropertyName("to")]
        public string To{ get;  set;   }
        [JsonPropertyName("link")]
        public string LinkToFile { get; set; }

        public override string ToString() => JsonSerializer.Serialize<MailModel>(this);
        
    }
}
