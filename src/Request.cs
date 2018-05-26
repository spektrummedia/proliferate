using System.Collections.Generic;
using Newtonsoft.Json;

namespace Proliferate
{
    public class Request
    {
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}