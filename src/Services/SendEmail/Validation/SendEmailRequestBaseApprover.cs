using System.Collections.Generic;

namespace Proliferate.Services.SendEmail.Validation
{
    public abstract class SendEmailRequestBaseApprover
    {
        protected Dictionary<string, string> ErrorsResult { get; set; }
        protected SendEmailRequestBaseApprover Successor;

        protected SendEmailRequestBaseApprover()
        {
            ErrorsResult = new Dictionary<string, string>();
        }

        public void SetSuccessor(SendEmailRequestBaseApprover successor)
        {
            Successor = successor;
        }

        public abstract Dictionary<string, string> Process(SendEmailRequest request);
    }
}