using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class OperatinFailedException : ExceptionBase
    {
        public OperatinFailedException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.OperationFailed;
    }
}
