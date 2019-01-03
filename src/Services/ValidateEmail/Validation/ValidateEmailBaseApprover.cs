using System.Collections.Generic;
using System.Threading.Tasks;
using Proliferate.Services.SendEmail;

namespace Proliferate.Services.ValidateApiKey.Validation
{
    public abstract class ValidateEmailBaseApprover
    {
        protected Dictionary<string, string> ErrorsResult { get; set; }
        protected ValidateEmailBaseApprover Successor;

        protected ValidateEmailBaseApprover()
        {
            ErrorsResult = new Dictionary<string, string>();
        }

        public void SetSuccessor(ValidateEmailBaseApprover successor)
        {
            Successor = successor;
        }

        public abstract Task<Dictionary<string, string>> Process(SendEmailRequest request);
    }
}