using Serilog;
using Utils.Guards;

namespace Logic.Services.Base
{
    public abstract class BaseService
    {
        protected ILogger Logger;
        public BaseService(ILogger logger)
        {
            Guard.GuardAgainstNull(logger, nameof(logger));
            Logger = logger;
        }
    }
}
