using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Api.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public UnauthorizedException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.UnAuthorized;
    }
}
