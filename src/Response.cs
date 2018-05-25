using System.Collections.Generic;
using Newtonsoft.Json;

namespace Proliferate
{
    public class Response
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; } = 200;

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }
            = new Dictionary<string, string> {{"Access-Control-Allow-Origin", "*"}};

        [JsonProperty("body")]
        public string Body { get; set; }

        public Response()
        {
        }

        public Response(object body)
        {
            Body = JsonConvert.SerializeObject(body);
        }
    }
}