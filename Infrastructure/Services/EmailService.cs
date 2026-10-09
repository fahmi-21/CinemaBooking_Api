using Application.Abstractions;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        var fromAddress = GetRequiredSetting("Email:FromAddress");
        var smtpUsername = GetRequiredSetting("Email:SmtpUsername");
        var smtpPassword = GetRequiredSetting("Email:SmtpPassword");
        var smtpHost = GetRequiredSetting("Email:SmtpHost");

        if (!int.TryParse(_configuration["Email:SmtpPort"], out var smtpPort))
        {
            throw new InvalidOperationException("Email SMTP port is not configured correctly.");
        }

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Cinema Booking", fromAddress));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(
            smtpHost,
            smtpPort,
            SecureSocketOptions.StartTls,
            cancellationToken);

        await smtp.AuthenticateAsync(
            smtpUsername,
            smtpPassword,
            cancellationToken);

        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }

    private string GetRequiredSetting(string key)
    {
        return _configuration[key]
            ?? throw new InvalidOperationException("Required email setting '" + key + "' is not configured.");
    }
}
