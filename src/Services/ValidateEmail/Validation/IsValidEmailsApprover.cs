using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Proliferate.Services.ValidateApiKey.Validation;
using Spk.Common.Helpers.String;

namespace Proliferate.Services.ValidateEmail.Validation
{
    public class IsValidEmailsApprover : ValidateEmailBaseApprover
    {
        private readonly ILambdaContext _context;

        public IsValidEmailsApprover(ILambdaContext context)
        {
            _context = context;
        }

        public override async Task<Dictionary<string, string>> Process(SendEmailRequest request)
        {
            var invalidToEmail = request.To.Where(x => !x.IsValidEmail());
            if (invalidToEmail.Any())
            {
                ErrorsResult.Add("to", $"Invalid emails: {string.Join(",", invalidToEmail)}");
            }

            var invalidCCEmail = request.CC.Where(x => !x.IsValidEmail());
            if (invalidCCEmail.Any())
            {
                ErrorsResult.Add("cc", $"Invalid emails: {string.Join(",", invalidToEmail)}");
            }

            var invalidBCCEmail = request.BCC.Where(x => !x.IsValidEmail());
            if (invalidBCCEmail.Any())
            {
                ErrorsResult.Add("bcc", $"Invalid emails: {string.Join(",", invalidBCCEmail)}");
            }

            if (ErrorsResult.Any())
            {
                return ErrorsResult;
            }

            if (Successor != null)
            {
                return await Successor.Process(request);
            }

            return ErrorsResult;
        }
    }
}