using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using MoreLinq;
using Proliferate.Services.SendEmail;
using Proliferate.Services.ValidateApiKey.Validation;
using Spk.Common.Helpers.Service;

namespace Proliferate.Services.ValidateApiKey
{
    public class ValidateApiKeyService : FunctionHandler
    {
        private readonly string _buildMarkupFunctionName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public ValidateApiKeyService()
        {
            _buildMarkupFunctionName = $"proliferate-{Stage}-build-markup";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public async Task<ServiceResult> Execute(SendEmailRequest request, ILambdaContext context)
        {
            var result = new ServiceResult();
            var errors = await ValidateApiKeyValidationContext.Validate(request, context);
            if (errors.Any())
            {
                errors.ForEach(error => result.AddError($"{error.Key}: {error.Value}"));
                return result;
            }

            try
            {
                var serviceResult = await _lambdaHandler.TriggerLambdaFunction(_buildMarkupFunctionName, request);
                if (!serviceResult.Success)
                {
                    serviceResult.Errors.ForEach(error => result.AddError(error));
                }
            }
            catch (Exception exception)
            {
                result.AddError(exception.Message);
            }

            return result;
        }
    }
}