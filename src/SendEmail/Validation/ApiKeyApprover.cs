using System.Collections.Generic;
using Spk.Common.Helpers.String;

namespace Proliferate.Validation
{
    public class ApiKeyApprover : SendEmailRequestBaseApprover
    {
        public override Dictionary<string, string> Process(SendEmailRequest request)
        {
            if (request.ApiKey.IsNullOrWhiteSpace())
            {
                ErrorsResult.Add("api_key", "is missing");
            }

            if (Successor != null)
            {
                return Successor.Process(request);
            }

            return ErrorsResult;
        }
    }
}