using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class OperationFailedException : ExceptionBase
    {
        public OperationFailedException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.OperationFailed;
    }
}
