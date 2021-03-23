using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions.ErrorCodes
{
    public enum ErrorCode
    {
        UserNotFound = 100,
        UserAlreadyExists = 101,


        ErrorInDbWhileSaving = 8888
    }
}
