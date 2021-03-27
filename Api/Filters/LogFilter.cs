using Api.Model;
using DAL;
using Domain.Exceptions.Base;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

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
            var request = GetRequestPathAndBody(context.HttpContext.Request);
            int? userId = null;

            if (context.HttpContext.User is AuthUser authUser)
                userId = authUser.Id;

            if (context.Exception != null)
                HandleException(context.Exception, request, userId);


            var response = GetResponseBody(context.HttpContext.Response);
            HandleEvent(request, response, userId);
        }

        private void HandleEvent(string request, string response, int? userId)
        {
            var log = new LogEntry(userId, request, response, 0);
            using(var db = _dbCreator())
            {
                db.LogEntries.Add(log);
                db.SaveChanges();
            }
        }

        private string GetResponseBody(HttpResponse httpResponse)
        {
            using(var streamReader = new StreamReader(httpResponse.Body))
            {
                return streamReader.ReadToEndAsync()
                    .GetAwaiter()
                    .GetResult();
            }
        }

        private void HandleException(Exception exception, string request, int? userId)
        {
            string response = string.Empty;
            int code = 0;

            if(exception is ExceptionBase exBase)
            {
                response = JsonConvert.SerializeObject(exBase);
                code = (int)exBase.Code;
            }

            else
            {
                var globalException = new GlobalException("Global exception");
                exception = globalException;
                response = JsonConvert.SerializeObject(globalException);
                code = (int)globalException.Code;
            }

            Log(request, response, code, userId);

            throw exception;

        }

        private string GetRequestPathAndBody(HttpRequest request)
        {
            var path = request.Path;
            string body;
            using(var streamReader = new StreamReader(request.Body))
            {
                body =  streamReader.ReadToEndAsync()
                    .GetAwaiter()
                    .GetResult();
            }

            return $"{path} {body}";
        }

        private void Log(string request, string response, int code, int? userId)
        {
            using(var db = _dbCreator())
            {
                var log = new LogEntry(userId, request, response, code);
                db.LogEntries.Add(log);
                db.SaveChanges();
            }
        }
    }
}
