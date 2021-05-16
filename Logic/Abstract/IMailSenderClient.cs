using Logic.Dto;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IMailSenderClient
    {
        Task<OperationResult> SendMail(MailDto mailDto);
    }
}
