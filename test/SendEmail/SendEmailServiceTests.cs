using AutoFixture.Xunit2;
using Newtonsoft.Json;
using Proliferate.SendEmail;
using Shouldly;
using Spk.Common.Helpers.Service;
using Xunit;

namespace Proliferate.Test.SendEmail
{
    public class SendEmailServiceTests
    {
        public SendEmailServiceTests()
        {
            _service = new SendEmailService();
        }

        private readonly SendEmailService _service;

        [Theory]
        [AutoData]
        public void Execute_ShouldFail_WhenMissingApiKey(SendEmailRequest request)
        {
            // Arrange
            request.ApiKey = null;

            // Act
            var result = _service.Execute(new Request
            {
                Body = JsonConvert.SerializeObject(request)
            }, null);

            // Assert
            JsonConvert.DeserializeObject<ServiceResult>(result.Body).Errors.ShouldNotBeEmpty();
        }

        [Theory]
        [AutoData]
        public void Execute_ShouldFail_WhenNoToEmail(SendEmailRequest request)
        {
            // Arrange
            request.To = null;

            // Act
            var result = _service.Execute(new Request
            {
                Body = JsonConvert.SerializeObject(request)
            }, null);

            // Assert
            JsonConvert.DeserializeObject<ServiceResult>(result.Body).Errors.ShouldNotBeEmpty();
        }

        [Theory]
        [AutoData]
        public void Execute_ShouldFail_WhenNoContent(SendEmailRequest request)
        {
            // Arrange
            request.Content = null;

            // Act
            var result = _service.Execute(new Request
            {
                Body = JsonConvert.SerializeObject(request)
            }, null);

            // Assert
            JsonConvert.DeserializeObject<ServiceResult>(result.Body).Errors.ShouldNotBeEmpty();
        }

        [Theory]
        [AutoData]
        public void Execute_ShouldFail_WhenContentHtmlAndTextEmpty(SendEmailRequest request)
        {
            // Arrange
            request.Content.Html = null;
            request.Content.Text = null;

            // Act
            var result = _service.Execute(new Request
            {
                Body = JsonConvert.SerializeObject(request)
            }, null);

            // Assert
            JsonConvert.DeserializeObject<ServiceResult>(result.Body).Errors.ShouldNotBeEmpty();
        }

        [Fact]
        public void Execute_ShouldFail_WhenNoPayload()
        {
            // Act
            var result = _service.Execute(new Request
            {
                Body = null
            }, null);

            // Assert
            JsonConvert.DeserializeObject<ServiceResult>(result.Body).Errors.ShouldNotBeEmpty();
        }
    }
}