using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace Proliferate
{
    public class AmazonLambdaHandler
    {
        public async Task<string> Bleh(string apiFunctionName)
        {
            var lambdaResult = string.Empty;

            using (var client = new AmazonLambdaClient())
            {
                var response = await client.InvokeAsync(new InvokeRequest
                {
                    FunctionName = apiFunctionName
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