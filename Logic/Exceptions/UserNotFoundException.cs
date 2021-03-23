using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class UserNotFoundException : ExceptionBase
    {
        public UserNotFoundException(string message) 
            : base(message)
        {
        }

        public override ErrorCode Code => ErrorCode.UserNotFound;
    }
}
