using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using MoreLinq;
using Newtonsoft.Json;
using Proliferate.Services.SendEmail.Validation;
using Spk.Common.Helpers.Service;

namespace Proliferate.Services.SendEmail
{
    public class SendEmailService : FunctionHandler
    {
        private readonly AmazonLambdaHandler _lambdaHandler;
        private readonly string _validateEmailFunctionName;

        public SendEmailService()
        {
            _validateEmailFunctionName = $"proliferate-{Stage}-validate-email";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public async Task<Response> Execute(Request payload, ILambdaContext context)
        {
            var result = new ServiceResult<string>();
            SendEmailRequest request = null;

            try
            {
                request = JsonConvert.DeserializeObject<SendEmailRequest>(payload.Body);
                context.Logger.Log($"Send email to {string.Join(";", request.To)}");
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
                return new Response(result);
            }

            var errors = SendEmailRequestValidationContext.Validate(request);
            if (errors.Any())
            {
                errors.ForEach(error => result.AddError($"{error.Key}: {error.Value}"));
                return new Response(result);
            }

            try
            {
                var serviceResult = await _lambdaHandler.TriggerLambdaFunction(_validateEmailFunctionName, request);
                if (!serviceResult.Success)
                {
                    serviceResult.Errors.ForEach(error => result.AddError(error));
                }
            }
            catch (Exception exception)
            {
                result.AddError(exception.Message);
            }


            return new Response(result);
        }
    }
}