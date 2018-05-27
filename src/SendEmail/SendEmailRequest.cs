using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

namespace Proliferate.SendEmail
{
    public class EmailContent
    {
        public string Text { get; set; }
        public string Html { get; set; }
    }

    public class SendEmailRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public List<string> To { get; set; }

        [JsonProperty("cc")]
        public List<string> CC { get; set; }

        [JsonProperty("bcc")]
        public List<string> BCC { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("content")]
        public EmailContent Content { get; set; }
    }
}