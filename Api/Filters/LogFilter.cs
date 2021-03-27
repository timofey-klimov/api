using Api.Dto.Response;
using Api.Model;
using DAL;
using Domain.Exceptions.Base;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


            var response = GetResponseBody(context.Result);
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

        private string GetResponseBody(IActionResult result)
        {
            var body = ((ObjectResult)result).Value;

            return JsonConvert.SerializeObject(body);
        }

        private void HandleException(Exception exception, string request, int? userId)
        {
            string response = string.Empty;
            int code = 0;

            if(exception is ExceptionBase exBase)
            {
                response = JsonConvert.SerializeObject(ApiResponse.Fail((int)exBase.Code, exBase.Message));
                code = (int)exBase.Code;
            }

            else
            {
                exception = new GlobalException("Global exception");
                var ex = (ExceptionBase)exception;
                response = JsonConvert.SerializeObject(ApiResponse.Fail((int)ex.Code, ex.Message));
                code = (int)ex.Code;
            }

            Log(request, response, code, userId);

            throw exception;

        }

        private string GetRequestPathAndBody(HttpRequest request)
        {
            ResetStream(request.Body);

            var path = request.Path;
            string body;

            using(var streamReader = new StreamReader(request.Body))
            {
                body =  streamReader.ReadToEndAsync()
                    .GetAwaiter()
                    .GetResult();

                ResetStream(request.Body);
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

        private void ResetStream(Stream stream)
        {
            stream.Position = 0;
        }
    }
}
