using System.Collections.Generic;
using Newtonsoft.Json;

namespace Proliferate
{
    public class EmailContent
    {
        public string text { get; set; }
        public string html { get; set; }
    }

    public class SendEmailRequest
    {
        [JsonProperty("api_key")]
        public string api_key { get; set; }

        [JsonProperty("from")]
        public string from { get; set; }

        [JsonProperty("to")]
        public List<string> to { get; set; }

        [JsonProperty("cc")]
        public List<string> cc { get; set; }

        [JsonProperty("bcc")]
        public List<string> bcc { get; set; }

        [JsonProperty("subject")]
        public string subject { get; set; }

        [JsonProperty("content")]
        public EmailContent content { get; set; }
    }
}