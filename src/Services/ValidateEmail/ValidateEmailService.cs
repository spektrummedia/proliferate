using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Spk.Common.Helpers.Service;

namespace Proliferate.Services.ValidateEmail
{
    public class ValidateEmailService : FunctionHandler
    {
        private readonly AmazonLambdaHandler _lambdaHandler;
        private readonly string _validateApiKeyFunctionName;

        public ValidateEmailService()
        {
            _validateApiKeyFunctionName = $"proliferate-{Stage}-validate-api-key";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public async Task<ServiceResult> Execute(SendEmailRequest request, ILambdaContext context)
        {
            var result = new ServiceResult();

            try
            {
                return await _lambdaHandler.TriggerLambdaFunction(_validateApiKeyFunctionName, request);
            }
            catch (Exception exception)
            {
                result.AddError(exception.Message);
            }

            return result;
        }
    }
}