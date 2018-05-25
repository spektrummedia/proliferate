using Amazon.Lambda.Core;

namespace Proliferate
{
    public class ValidateApiKey : FunctionHandler
    {
        public Response Execute(Request request, ILambdaContext context)
        {
            return new Response("ValidateApiKey");
        }
    }
}