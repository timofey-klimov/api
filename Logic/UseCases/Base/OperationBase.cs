using DAL;
using Domain.Exceptions.Base;
using Logic.Exceptions;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Operations.Base
{
    public abstract class OperationBase<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest: IRequest<TResult>
    {
        protected Func<DatabaseContext> DbCreator;
        protected ILogger Logger;
        public OperationBase(Func<DatabaseContext> dbCreator, ILogger logger)
        {
            DbCreator = dbCreator;
            Logger = logger;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleCore(request, cancellationToken);
            }
            catch(Exception ex)
            {
                Logger.Error($"Error in operation {this.GetType().Name}\n{ex.Message}");
                switch (ex)
                {
                    case ExceptionBase exBase:
                        throw exBase;
                    default:
                        var operationError = new OperationFailedException($"{this.GetType().Name} failed");
                        throw operationError;
                }
            }
        }

        protected abstract Task<TResult> HandleCore(TRequest request, CancellationToken token);
    }
}
