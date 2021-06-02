using Api.Attributes;
using Api.Dto.Response;
using Api.Model;
using DAL;
using Domain.Exceptions.Base;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Utils.Extensions;

namespace Api.Filters
{
    public class LogEntryFilter : ActionFilterAttribute
    {
        private readonly Func<DatabaseContext> _dbCreator;
        public LogEntryFilter(Func<DatabaseContext> dbCreator)
        {
            _dbCreator = dbCreator;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var actionDescriptor = context
               .HttpContext
               .GetEndpoint()
               .Metadata
               .GetOrderedMetadata<ControllerActionDescriptor>()
               .FirstOrDefault();

            if (actionDescriptor.MethodInfo.GetCustomAttribute<UnableLoggingAttribute>() != null)
                return;

            HandleCore(context, actionDescriptor);
        }

        private void HandleCore(ActionExecutedContext context, ControllerActionDescriptor descriptor)
        {
            var request = GetRequestPathAndBody(context.HttpContext, descriptor);
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
            switch (result)
            {
                case OkObjectResult okObj:
                    return JsonConvert.SerializeObject(okObj.Value);
                default:
                    return string.Empty;
            }
        }

        private void HandleException(Exception exception, string request, int? userId)
        {
            string response = string.Empty;
            int code = 0;

            switch (exception)
            {
                case ExceptionBase exBase:
                    code = exBase.Code.To<int>();
                    response = JsonConvert.SerializeObject(ApiResponse.Fail(code, exBase.Message));
                    break;

                default:
                    exception = new GlobalException("Global exception");
                    var ex = exception.To<ExceptionBase>();
                    code = ex.Code.To<int>();
                    response = JsonConvert.SerializeObject(ApiResponse.Fail(code, ex.Message));
                    break;
            };

            Log(request, response, code, userId);

            throw exception;

        }

        private string GetRequestPathAndBody(HttpContext context, ControllerActionDescriptor descriptor)
        {
            var request = context.Request;
            ResetStream(request.Body);

            var path = request.Path;
            string body = string.Empty;

            if (descriptor.MethodInfo.GetCustomAttribute<LogWithoutBodyAttribute>() == null)
                body = new StreamReader(request.Body).ReadToEnd();
          
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
