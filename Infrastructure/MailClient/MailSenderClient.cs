using ApplicationSettings.Settings;
using Logic.Abstract;
using Logic.Dto;
using MailKit.Net.Smtp;
using MimeKit;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Infrastructure.MailClient
{
    public class MailSenderClient : IMailSenderClient
    {
        private readonly MailClientSettings _settings;
        private readonly ILogger _logger;
        public MailSenderClient(MailClientSettings settings, ILogger logger)
        {
            _logger = logger;
            _settings = settings;
        }
        public async Task<OperationResult> SendMail(MailDto mailDto)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_settings.SenderName, _settings.EmailSender));
            email.To.Add(new MailboxAddress(mailDto.MailAddress));
            email.Subject = mailDto.Header;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = mailDto.Body
            };

            try
            {

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_settings.Host, _settings.Port);
                    await client.AuthenticateAsync(_settings.EmailSender, _settings.Password);
                    await client.SendAsync(email);
                }

                return OperationResult.Success();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return OperationResult.Fail("MailSenderClient failed");
            }
        }
    }
}
