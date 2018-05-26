using System;
using System.Linq;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Proliferate.Validation;
using Spk.Common.Helpers.Service;
using Spk.Common.Helpers.String;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace Proliferate.SendEmail
{
    public class SendEmailService : FunctionHandler
    {
        private readonly ServiceResult<SendEmailRequest> _result;

        public SendEmailService()
        {
            _result = new ServiceResult<SendEmailRequest>();
        }

        public Response Execute(Request payload, ILambdaContext context)
        {
            SendEmailRequest request = null;

            try
            {
                request = JsonConvert.DeserializeObject<SendEmailRequest>(payload.Body);
            }
            catch (Exception e)
            {
                _result.AddError(e.Message);
                return new Response(_result);
            }

            var errors = SendEmailRequestValidationContext.Validate(request);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    _result.AddError($"{error.Key}: {error.Value}");
                }
                return new Response(_result);
            }

            return new Response(_result);
        }
    }
}