using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this._emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
    }

    public async Task<bool> SendEmailAsync(Email email)
    {
        // example: https://github.com/sendgrid/sendgrid-csharp/blob/main/ExampleCoreProject/Program.cs
        var client = new SendGridClient(this._emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = this._emailSettings.FromAddress,
            Name = this._emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        if (response.IsSuccessStatusCode)
        {
            this._logger.LogInformation($"Email sent to '{to}'");
        }
        else
        {
            this._logger.LogWarning($"Email failed to send: {response.StatusCode} - {response.Body}");
        }

        return response.IsSuccessStatusCode;
    }
}
