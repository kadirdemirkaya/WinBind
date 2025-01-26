using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WinBind.Application.Abstractions;
using WinBind.Domain.Models.Options;

namespace WinBind.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpOptions _smtpOptions;
        public EmailService(SmtpOptions smtpOptions)
        {
            _smtpOptions = smtpOptions;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Celebi Seyehat", _smtpOptions.User));
            //message.To.Add(new MailboxAddress("", toEmail));
            //message.Subject = subject;

            //var bodyBuilder = new BodyBuilder { HtmlBody = body };
            //message.Body = bodyBuilder.ToMessageBody();

            //using var client = new SmtpClient();
            //await client.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, SecureSocketOptions.StartTls);
            //await client.AuthenticateAsync(_smtpOptions.User, _smtpOptions.Pass);
            //await client.SendAsync(message);
            //await client.DisconnectAsync(true);
        }
    }
}
