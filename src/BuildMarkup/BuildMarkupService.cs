using Amazon.Lambda.Core;

namespace Proliferate.BuildMarkup
{
    public class BuildMarkupService : FunctionHandler
    {
        private readonly string _validateApiKeyFunctionName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public BuildMarkupService()
        {
            _validateApiKeyFunctionName = $"proliferate-{Stage}-validate-api-key";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public Response Execute(Request request, ILambdaContext context)
        {
            return new Response("BuildMarkupService");
        }
    }
}