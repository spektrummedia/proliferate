using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;
using Spk.Common.Helpers.Service;

namespace Proliferate
{
    public class AmazonLambdaHandler
    {
        public async Task<ServiceResult> TriggerLambdaFunction<T>(string apiFunctionName, T payload)
        {
            using (var client = new AmazonLambdaClient())
            {
                var response = await client.InvokeAsync(new InvokeRequest
                {
                    FunctionName = apiFunctionName,
                    Payload = JsonConvert.SerializeObject(payload)
                });

                using (var sr = new StreamReader(response.Payload))
                {
                    return JsonConvert.DeserializeObject<ServiceResult>(sr.ReadToEnd());
                }
            }
        }
    }
}