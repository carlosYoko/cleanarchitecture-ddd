using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings? emailSettings; { get; }
        public ILogger<EmailService>? logger { get; }

        public EmailService(IOptions<EmailSettings?> emailSettings, ILogger<EmailService>? logger)
        {
            this.emailSettings = emailSettings.Value;
            this.logger = logger;
        }

        public async Task<bool> SendEmail(Application.Models.Email email)
        {
            var client = new SendGridClient(emailSettings!.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress()
            {
                Email = emailSettings.FromAdress,
                Name = emailSettings.FromName,
            };

            var sendgridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendgridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            };

            logger!.LogError("EL email no se ha podido enviar");
            return false;
        }
    }
}
