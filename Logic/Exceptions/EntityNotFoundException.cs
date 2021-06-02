using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;
using System;

namespace Logic.Exceptions
{
    public class EntityNotFoundException : ExceptionBase
    {
        public EntityNotFoundException(string message)
            :base(message)
        {

        }
        public override ErrorCode Code => ErrorCode.EntityNotFound;
    }
}
