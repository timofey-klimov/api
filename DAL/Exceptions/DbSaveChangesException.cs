using Domain.Exceptions.Base;
using Domain.Exceptions.ErrorCodes;

namespace DAL.Exceptions
{
    public class DbSaveChangesException : ExceptionBase
    {
        public DbSaveChangesException(string message)
            :base(message)
        {

        }

        public override ErrorCode Code => ErrorCode.ErrorInDbWhileSaving;
    }
}
