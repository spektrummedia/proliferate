using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Newtonsoft.Json;
using Proliferate.SendEmail;
using Spk.Common.Helpers.Service;

namespace Proliferate.ValidateEmail
{
    public class ValidateEmailService : FunctionHandler
    {
        private readonly string _validateApiKeyFunctionName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public ValidateEmailService()
        {
            _validateApiKeyFunctionName = $"proliferate-{Stage}-validate-api-key";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public async Task<Response> Execute(SendEmailRequest request, ILambdaContext context)
        {
            context.Logger.Log($"KEY: {request.ApiKey}");
            var result = new ServiceResult<object>();

            try
            {
                result.SetData(new
                {
                    Stage,
                    FunctionResponse = await _lambdaHandler.TriggerLambdaFunction(_validateApiKeyFunctionName, request)
                });
            }
            catch (Exception exception)
            {
                result.AddError(exception.Message);
            }

            return new Response(result);
        }
    }
}