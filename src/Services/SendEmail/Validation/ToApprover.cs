using System.Collections.Generic;
using System.Linq;

namespace Proliferate.Services.SendEmail.Validation
{
    public class ToApprover : SendEmailRequestBaseApprover
    {
        public override Dictionary<string, string> Process(SendEmailRequest request)
        {
            if (request.To == null || !request.To.Any())
            {
                ErrorsResult.Add("To", "is missing");
                return ErrorsResult;
            }

            if (Successor != null)
            {
                return Successor.Process(request);
            }

            return ErrorsResult;
        }
    }
}