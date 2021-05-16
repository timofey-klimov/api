using ApplicationSettings.Settings;
using Infrastructure.MailClient;
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
        }

        [Test]
        public void SendEmail()
        {
            var mailClient = new MailSenderClient(_settings);
            var result = mailClient.SendMail(new Logic.Dto.MailDto("hi", "<div>1111</div>", "timofey.klimov@inbox.ru")).Result;


            Assert.AreEqual(true, result.Succsess);
        }
    }
}
