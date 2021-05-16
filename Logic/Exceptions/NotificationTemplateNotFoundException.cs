using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class NotificationTemplateNotFoundException : ExceptionBase
    {
        public NotificationTemplateNotFoundException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.NotificationTemplateWasNotFind;
    }
}
