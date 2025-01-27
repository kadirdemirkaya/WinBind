using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Ocsp;
using WinBind.Application.Abstractions;
using WinBind.Application.Features.Commands.Requests;
using WinBind.Domain.Enums;
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

        public async Task SendEmailAsync(SendEmailCommandRequest request)
        {
            string subject = string.Empty;
            string body = string.Empty;

            switch (request.EmailType)
            {
                case EmailType.Registration:
                    subject = "Kayıt Başarılı";
                    body = $"Merhaba {request.UserFirstName}, hesabınız başarıyla oluşturuldu!";
                    break;

                case EmailType.Purchase:
                    subject = "Satın Alma Başarılı";
                    body = $"Satın aldığınız {request.PurchasedProductName} için teşekkürler. İyi günler dileriz.";
                    break;

                default:
                    throw new ArgumentException("Geçersiz e-posta türü.");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Celebi Seyehat", _smtpOptions.User));
            message.To.Add(new MailboxAddress("", request.ToEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = body };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpOptions.Server, _smtpOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpOptions.User, _smtpOptions.Pass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
