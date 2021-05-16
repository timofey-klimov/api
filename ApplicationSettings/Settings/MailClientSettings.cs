using System.Net;

namespace ApplicationSettings.Settings
{
    public class MailClientSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string EmailSender { get; set; }

        public string Password { get; set; }

        public string SenderName { get; set; }
    }
}
