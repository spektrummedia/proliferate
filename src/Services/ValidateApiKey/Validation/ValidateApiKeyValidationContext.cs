using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Proliferate.Services.ValidateEmail.Validation;

namespace Proliferate.Services.ValidateApiKey.Validation
{
    public class ValidateApiKeyValidationContext
    {
        public static async Task<Dictionary<string, string>> Validate(SendEmailRequest request, ILambdaContext context)
        {
            var isValidEmailApprover = new IsValidEmailsApprover(context);
            return await isValidEmailApprover.Process(request);
        }
    }
}