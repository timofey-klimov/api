using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class UserAlreadyExistsException : ExceptionBase
    {
        public UserAlreadyExistsException(string message)
            :base(message)
        {

        }

        public override ErrorCode Code => ErrorCode.UserAlreadyExists;
    }
}
