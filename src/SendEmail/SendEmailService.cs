using System;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Spk.Common.Helpers.Service;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Proliferate
{
    public class SendEmailService : FunctionHandler
    {
        public Response Execute(Request request, ILambdaContext context)
        {
            var result = new ServiceResult<SendEmailRequest>();

            try
            {
                var payload = JsonConvert.DeserializeObject<SendEmailRequest>(request.Body);
                result.SetData(payload);
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }

            return new Response(result);
        }
    }
}