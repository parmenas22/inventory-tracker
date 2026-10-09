using System.Net;
using System.Net.Mail;

namespace api.Services.Auth
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendLoginCodeAsync(string email, string code, CancellationToken cancellationToken = default)
        {
            var host = _configuration["Email:Smtp:Host"];
            var fromAddress = _configuration["Email:FromAddress"];
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(fromAddress))
            {
                throw new InvalidOperationException("SMTP host and email sender address must be configured.");
            }

            var port = _configuration.GetValue<int?>("Email:Smtp:Port") ?? 587;
            var userName = _configuration["Email:Smtp:UserName"];
            var password = _configuration["Email:Smtp:Password"];
            var enableSsl = _configuration.GetValue<bool?>("Email:Smtp:EnableSsl") ?? true;

            using var message = new MailMessage(fromAddress, email)
            {
                Subject = "Your inventory tracker sign-in code",
                Body = $"Your sign-in code is {code}. It expires in 10 minutes.",
                IsBodyHtml = false
            };
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = enableSsl,
                Credentials = string.IsNullOrWhiteSpace(userName)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(userName, password)
            };

            await client.SendMailAsync(message, cancellationToken);
        }
    }
}
