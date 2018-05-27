using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Proliferate.Services.SendEmail;
using Proliferate.Utilities;
using Spk.Common.Helpers.Service;

namespace Proliferate.Services.BuildMarkup
{
    public class BuildMarkupService : FunctionHandler
    {
        private readonly string _emailsTableName;
        private readonly AmazonLambdaHandler _lambdaHandler;

        public BuildMarkupService()
        {
            _emailsTableName = $"proliferate-{Stage}-emails";
            _lambdaHandler = new AmazonLambdaHandler();
        }

        public async Task<ServiceResult> Execute(SendEmailRequest request, ILambdaContext context)
        {
            using (var dbClient = new AmazonDynamoDBClient())
            {
                request.Content.Html = DataCompressor.Compress(request.Content.Html);
                request.Content.Text = DataCompressor.Compress(request.Content.Text);

                var response = await dbClient.PutItemAsync(_emailsTableName, new Dictionary<string, AttributeValue>
                {
                    {"id", new AttributeValue(Guid.NewGuid().ToString())},
                    {"from", new AttributeValue(request.From)},
                    {"subject", new AttributeValue(request.Subject)},
                    {"api_key", new AttributeValue(request.ApiKey)},
                    {"to", new AttributeValue(request.To)},
                    {"cc", new AttributeValue(request.CC)},
                    {"bcc", new AttributeValue(request.BCC)},
                    {"content_html", new AttributeValue(request.Content.Html)},
                    {"content_text", new AttributeValue(request.Content.Text)}
                });

                return new ServiceResult();
            }
        }
    }
}