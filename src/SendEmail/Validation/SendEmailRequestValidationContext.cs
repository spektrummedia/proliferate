using System.Collections.Generic;

namespace Proliferate.Validation
{
    public static class SendEmailRequestValidationContext
    {
        public static Dictionary<string, string> Validate(SendEmailRequest request)
        {
            var emailToApprover = new ToApprover();
            var emailContentApprover = new ContentApprover();
            emailToApprover.SetSuccessor(emailContentApprover);

            var emailApiKeyApprover = new ApiKeyApprover();
            emailContentApprover.SetSuccessor(emailApiKeyApprover);
            return emailToApprover.Process(request);
        }
    }
}