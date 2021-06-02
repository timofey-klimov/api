using ApplicationSettings.Settings;
using Infrastructure.MailClient;
using Logic.Dto;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Infrastructure
{
    [TestFixture]
    public class MailClientTests
    {
        private MailClientSettings _settings;
        private Mock<Serilog.ILogger> _logger;

        [SetUp]
        public void Setup()
        {
            _settings = new MailClientSettings()
            {
                EmailSender = "timofey.klimov@inbox.ru",
                Password = "tima22282",
                Host = "smtp.mail.ru",
                Port = 465,
                SenderName = "Сервис"
            };
            _logger = new Mock<Serilog.ILogger>();
        }

        [Test]
        public void SendEmail()
        {
            var mailClient = new MailSenderClient(_settings, _logger.Object);
            var result = mailClient.SendMail(new MailDto("hi", "<div>1111</div>", "timofey.klimov@inbox.ru")).Result;


            Assert.AreEqual(true, result.IsSuccess);
        }
    }
}
