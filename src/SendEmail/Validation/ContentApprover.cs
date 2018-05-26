using System.Collections.Generic;
using Spk.Common.Helpers.String;

namespace Proliferate.SendEmail.Validation
{
    public class ContentApprover : SendEmailRequestBaseApprover
    {
        public override Dictionary<string, string> Process(SendEmailRequest request)
        {
            if (request.Content == null)
            {
                ErrorsResult.Add("Content", "is missing");
                return ErrorsResult;
            }

            if (request.Content.Html.IsNullOrWhiteSpace()
                && request.Content.Text.IsNullOrWhiteSpace())
            {
                ErrorsResult.Add("Content", "is empty");
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