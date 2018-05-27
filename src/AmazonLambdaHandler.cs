using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Newtonsoft.Json;

namespace Proliferate
{
    public class AmazonLambdaHandler
    {
        public async Task<string> TriggerLambdaFunction<T>(string apiFunctionName, T payload)
        {
            var lambdaResult = string.Empty;

            using (var client = new AmazonLambdaClient())
            {
                var response = await client.InvokeAsync(new InvokeRequest
                {
                    FunctionName = apiFunctionName,
                    Payload = JsonConvert.SerializeObject(payload)
                });


                using (var sr = new StreamReader(response.Payload))
                {
                    lambdaResult = sr.ReadToEnd();
                }
            }

            return lambdaResult;
        }
    }
}