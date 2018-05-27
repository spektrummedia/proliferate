using System.Collections.Generic;
using System.Threading.Tasks;
using Proliferate.Services.SendEmail;

namespace Proliferate.Services.ValidateApiKey.Validation
{
    public abstract class ValidateApiKeyBaseApprover
    {
        protected Dictionary<string, string> ErrorsResult { get; set; }
        protected ValidateApiKeyBaseApprover Successor;

        protected ValidateApiKeyBaseApprover()
        {
            ErrorsResult = new Dictionary<string, string>();
        }

        public void SetSuccessor(ValidateApiKeyBaseApprover successor)
        {
            Successor = successor;
        }

        public abstract Task<Dictionary<string, string>> Process(SendEmailRequest request);
    }
}