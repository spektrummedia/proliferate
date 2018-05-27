using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Proliferate.Services.SendEmail.Validation;

namespace Proliferate.Services.ValidateApiKey.Validation
{
    public class ValidateApiKeyValidationContext
    {
        public static async Task<Dictionary<string, string>> Validate(SendEmailRequest request, ILambdaContext context)
        {
            var apiKeyApprover = new ApiKeyApprover(context);
            return await apiKeyApprover.Process(request);
        }
    }
}