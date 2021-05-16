using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Logic.Exceptions
{
    public class CodeIsIncorrectException : ExceptionBase
    {
        public CodeIsIncorrectException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.IncorrectCode;
    }
}
