using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Spk.Common.Helpers.Service;

namespace Proliferate.ValidateApiKey
{
    public class ValideApiKeyService : FunctionHandler
    {
        private readonly string _buildMarkupFunctionName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public ValideApiKeyService()
        {
            _buildMarkupFunctionName = $"proliferate-{Stage}-build-markup";
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
                    FunctionResponse = await _lambdaHandler.TriggerLambdaFunction(_buildMarkupFunctionName)
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