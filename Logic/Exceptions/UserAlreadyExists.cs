using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class UserAlreadyExists : ExceptionBase
    {
        public UserAlreadyExists(string message)
            :base(message)
        {

        }

        public override ErrorCode Code => ErrorCode.UserAlreadyExists;
    }
}
