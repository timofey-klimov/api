using Domain.Exceptions.ErrorCodes;

namespace Domain.Exceptions.Base
{
    public class GlobalException : ExceptionBase
    {
        public GlobalException(string message)
            :base(message)
        {

        }

        public override ErrorCode Code => ErrorCode.Global;
    }
}
