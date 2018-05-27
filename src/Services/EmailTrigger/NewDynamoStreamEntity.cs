using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Proliferate.Services.EmailTrigger
{
    public partial class NewDynamoStreamEntity
    {
        [JsonProperty("Records")]
        public Record[] Records { get; set; }
    }

    public class Record
    {
        [JsonProperty("eventID")]
        public string EventId { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventVersion")]
        public string EventVersion { get; set; }

        [JsonProperty("eventSource")]
        public string EventSource { get; set; }

        [JsonProperty("awsRegion")]
        public string AwsRegion { get; set; }

        [JsonProperty("dynamodb")]
        public Dynamodb Dynamodb { get; set; }

        [JsonProperty("eventSourceARN")]
        public string EventSourceArn { get; set; }
    }

    public class Dynamodb
    {
        [JsonProperty("ApproximateCreationDateTime")]
        public long ApproximateCreationDateTime { get; set; }

        [JsonProperty("Keys")]
        public Keys Keys { get; set; }

        [JsonProperty("NewImage")]
        public NewImage NewImage { get; set; }

        [JsonProperty("SequenceNumber")]
        public string SequenceNumber { get; set; }

        [JsonProperty("SizeBytes")]
        public long SizeBytes { get; set; }

        [JsonProperty("StreamViewType")]
        public string StreamViewType { get; set; }
    }

    public class Keys
    {
        [JsonProperty("id")]
        public Id Id { get; set; }
    }

    public class Id
    {
        [JsonProperty("S")]
        public string S { get; set; }
    }

    public class NewImage
    {
        [JsonProperty("cc")]
        public Bcc Cc { get; set; }

        [JsonProperty("content_html")]
        public Id HtmlContent { get; set; }

        [JsonProperty("bcc")]
        public Bcc Bcc { get; set; }

        [JsonProperty("api_key")]
        public Id ApiKey { get; set; }

        [JsonProperty("from")]
        public Id From { get; set; }

        [JsonProperty("subject")]
        public Id Subject { get; set; }

        [JsonProperty("content_text")]
        public Id TextContent { get; set; }

        [JsonProperty("id")]
        public Id Id { get; set; }

        [JsonProperty("to")]
        public Bcc To { get; set; }
    }

    public class Bcc
    {
        [JsonProperty("SS")]
        public string[] Ss { get; set; }
    }

    public partial class NewDynamoStreamEntity
    {
        public static NewDynamoStreamEntity FromJson(string json)
        {
            return JsonConvert.DeserializeObject<NewDynamoStreamEntity>(json, Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this NewDynamoStreamEntity self)
        {
            return JsonConvert.SerializeObject(self, Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };
    }
}