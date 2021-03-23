using Domain.Exceptions.ErrorCodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.Base
{
    public abstract class ExceptionBase : Exception
    {
        public ExceptionBase(string message)
            :base(message)
        {

        }

        public abstract ErrorCode Code { get; }
    }
}
