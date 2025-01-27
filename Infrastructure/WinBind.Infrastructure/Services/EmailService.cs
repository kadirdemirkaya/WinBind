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
                    body = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Kayıt Başarılı</title>\r\n    <style>\r\n        body {{\r\n            font-family: Arial, sans-serif;\r\n            line-height: 1.6;\r\n            margin: 0;\r\n            padding: 0;\r\n            background-color: #f4f4f9;\r\n            color: #333;\r\n        }}\r\n        .email-container {{\r\n            max-width: 600px;\r\n            margin: 20px auto;\r\n            padding: 20px;\r\n            background: #ffffff;\r\n            border-radius: 8px;\r\n            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);\r\n        }}\r\n        .header {{\r\n            text-align: center;\r\n            padding: 10px 0;\r\n            border-bottom: 1px solid #dddddd;\r\n        }}\r\n        .header img {{\r\n            max-width: 150px;\r\n        }}\r\n        .content {{\r\n            padding: 20px;\r\n            text-align: center;\r\n        }}\r\n        .button {{\r\n            display: inline-block;\r\n            margin-top: 20px;\r\n            padding: 10px 20px;\r\n            background: #007bff;\r\n            color: #ffffff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-size: 16px;\r\n        }}\r\n        .button:hover {{\r\n            background: #0056b3;\r\n        }}\r\n        .footer {{\r\n            margin-top: 20px;\r\n            text-align: center;\r\n            font-size: 12px;\r\n            color: #666666;\r\n        }}\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <img src=\"https://example.com/logo.png\" alt=\"Site Logosu\">\r\n        </div>\r\n        <div class=\"content\">\r\n            <h1>Hoş Geldiniz!</h1>\r\n            <p>Merhaba,</p>\r\n            <p>Sitemize kayıt olduğunuz için teşekkür ederiz. Aşağıdaki bağlantıya tıklayarak hesabınızı doğrulayabilirsiniz:</p>\r\n            <a href=\"https://example.com/verify?token=YOUR_VERIFICATION_TOKEN\" class=\"button\">Hesabımı Doğrula</a>\r\n            <p>Bu bağlantı, güvenliğinizi sağlamak için 24 saat geçerlidir.</p>\r\n            <p>Herhangi bir sorunuz varsa, bizimle iletişime geçmekten çekinmeyin.</p>\r\n            <p>Teşekkürler,<br>Site Ekibi</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2025 Example.com. Tüm hakları saklıdır.</p>\r\n            <p>Bu e-posta otomatik olarak oluşturulmuştur, lütfen yanıtlamayın.</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";
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
