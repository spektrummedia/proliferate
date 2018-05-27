﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Proliferate.EmailTrigger
{
    public class EmailTriggerService : FunctionHandler
    {
        public async Task<Response> Execute(NewDynamoStreamEntity request, ILambdaContext context)
        {
            if (request == null || request.Records == null)
            {
                return new Response("invalid request");
            }

            if (!request.Records.Any() || request.Records.All(x => x.Dynamodb?.NewImage == null))
            {
                return new Response("no records");
            }

            var records = request.Records
                .Where(x => x.Dynamodb?.NewImage != null)
                .Where(x => x.EventName == "INSERT");

            using (var client = new AmazonSimpleEmailServiceClient())
            {
                foreach (var record in records)
                {
                    var sendRequest = new SendEmailRequest();
                    sendRequest.Source = record.Dynamodb.NewImage.From.S;
                    sendRequest.Destination = new Destination
                    {
                        ToAddresses = record.Dynamodb.NewImage.To.Ss.ToList(),
                        CcAddresses = record.Dynamodb.NewImage.Cc.Ss.ToList(),
                        BccAddresses = record.Dynamodb.NewImage.Bcc.Ss.ToList()
                    };
                    sendRequest.Message = new Message
                    {
                        Subject = new Content(record.Dynamodb.NewImage.Subject.S),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = record.Dynamodb.NewImage.HtmlContent.S
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = record.Dynamodb.NewImage.HtmlText.S
                            }
                        }
                    };

                    try
                    {
                        context.Logger.Log("Sending email using Amazon SES...");
                        var response = await client.SendEmailAsync(sendRequest);
                        context.Logger.Log($"The email was sent successfully. MessageId: {response.MessageId}");
                    }
                    catch (Exception ex)
                    {
                        context.Logger.Log($"The email was not sent. Reason: {ex.Message}");
                    }
                }
            }

            return new Response();
        }
    }
}