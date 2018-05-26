using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
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

        public async Task<Response> Execute(Request request, ILambdaContext context)
        {
            var result = new ServiceResult<object>();

            try
            {
                result.SetData(new
                {
                    Stage,
                    FunctionResponse = await _lambdaHandler.TriggerLambdaFunction(_validateApiKeyFunctionName)
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