using Application.Abstractions.Persistence;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(
        string to,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        var email = new MimeMessage();

        email.From.Add(
            new MailboxAddress(
                "Cinema Booking",
                "veno1udemy@gmail.com"));

        email.To.Add(
            MailboxAddress.Parse(to));

        email.Subject = subject;

        email.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        await smtp.ConnectAsync(
            "smtp.gmail.com",
            587,
            MailKit.Security.SecureSocketOptions.StartTls,
            cancellationToken);

        await smtp.AuthenticateAsync(
            "veno1udemy@gmail.com",
            "fpsc itfe fjih ceai",
            cancellationToken);

        await smtp.SendAsync(email, cancellationToken);

        await smtp.DisconnectAsync(true, cancellationToken);
    }
}