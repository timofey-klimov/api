using DAL;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Api.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        private readonly Func<DatabaseContext> _dbCreator;
        public LogFilter(Func<DatabaseContext> dbCreator)
        {
            _dbCreator = dbCreator;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
