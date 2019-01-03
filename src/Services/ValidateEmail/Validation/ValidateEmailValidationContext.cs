using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Proliferate.Services.ValidateApiKey.Validation;

namespace Proliferate.Services.ValidateEmail.Validation
{
    public class ValidateEmailValidationContext
    {
        public static async Task<Dictionary<string, string>> Validate(SendEmailRequest request, ILambdaContext context)
        {
            var apiKeyApprover = new ApiKeyApprover(context);
            return await apiKeyApprover.Process(request);
        }
    }
}