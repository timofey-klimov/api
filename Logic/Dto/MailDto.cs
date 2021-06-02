using Utils.Guards;

namespace Logic.Dto
{
    public class MailDto
    {
        public string Header { get; private set; }

        public string Body { get; private set; }

        public string MailAddress { get; private set; }

        public MailDto(string header, string body, string mailAddress)
        {
            Guard.GuardAgainstNull(header, nameof(header));
            Guard.GuardAgainstNull(body, nameof(body));
            Guard.GuardAgainstNull(mailAddress, nameof(mailAddress));

            Header = header;
            Body = body;
            MailAddress = mailAddress;
        }
    }
}
