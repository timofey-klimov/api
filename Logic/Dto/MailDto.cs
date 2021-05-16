namespace Logic.Dto
{
    public class MailDto
    {
        public string Header { get; private set; }

        public string Body { get; private set; }

        public string MailAddress { get; private set; }

        public MailDto(string header, string body, string mailAddress)
        {
            Header = header;
            Body = body;
            MailAddress = mailAddress;
        }
    }
}
