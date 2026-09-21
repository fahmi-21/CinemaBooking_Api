using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;

namespace Infrastructure.Services
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(
            string to,
            string subject,
            string body)
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
                MailKit.Security.SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                "veno1udemy@gmail.com",
                "fpsc itfe fjih ceai");

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}
