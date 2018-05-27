using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;

namespace Proliferate.Services.ValidateApiKey.Validation
{
    public class ApiKeyApprover : ValidateApiKeyBaseApprover
    {
        private readonly ILambdaContext _context;

        public ApiKeyApprover(ILambdaContext context)
        {
            _context = context;
        }

        public override async Task<Dictionary<string, string>> Process(SendEmailRequest request)
        {
            using (var dbClient = new AmazonDynamoDBClient())
            {
                var result = await dbClient.GetItemAsync(
                    $"proliferate-{Environment.GetEnvironmentVariable("stage")}-api-keys",
                    new Dictionary<string, AttributeValue>
                    {
                        {"api_key", new AttributeValue(request.ApiKey)}
                    });

                if (!result.IsItemSet)
                {
                    ErrorsResult.Add("api_key", "invalid api key");
                    return ErrorsResult;
                }
            }

            if (Successor != null)
            {
                return await Successor.Process(request);
            }

            return ErrorsResult;
        }
    }
}