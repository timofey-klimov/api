using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace Api.Exceptions
{
    public class TokenValidationFailedException : ExceptionBase
    {
        public TokenValidationFailedException(string message)
            :base(message)
        {

        }

        public override ErrorCode Code => ErrorCode.TokenValidationFailed;
    }
}
