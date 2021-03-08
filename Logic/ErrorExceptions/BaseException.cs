using System;

namespace Logic.ErrorExceptions
{
    public abstract class BaseException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }

        public BaseException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public BaseException(ErrorCode errorCode, string message)
            :base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
