using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Spk.Common.Helpers.Service;

namespace Proliferate
{
    public class ValidateEmail : FunctionHandler
    {
        private readonly string _apiKeyFunctionName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public ValidateEmail()
        {
            _apiKeyFunctionName = $"proliferate-{Stage}-validate-api-key";
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
                    FunctionResponse = await _lambdaHandler.Bleh(_apiKeyFunctionName)
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