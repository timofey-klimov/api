using Api.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var actionMethod = context
                .GetEndpoint()
                .Metadata
                .GetOrderedMetadata<ControllerActionDescriptor>()
                .FirstOrDefault();

            if (actionMethod == null || actionMethod.MethodInfo.GetCustomAttribute<UnableLoggingAttribute>() != null)
                return;

            var path = context.Request.Path.Value;
            var contentType = GetContentType(context.Request.Headers["Content-Type"]);
            var userAgent = context.Request.Headers["User-Agent"].ToString();

            var logger = context.RequestServices.GetService(typeof(ILogger)) as ILogger;

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            string body = string.Empty;

            if(actionMethod.MethodInfo.GetCustomAttribute<LogWithoutBodyAttribute>() == null)
                body = await new StreamReader(context.Request.Body).ReadToEndAsync();

            var log = BuildLogTemplate(body, path, userAgent, contentType);

            logger.Information(log);

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            await _next(context);
        }

        private string BuildLogTemplate(string body, string path, string userAgent, string contentType)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Get New request");
            stringBuilder.AppendLine($"Path:{path}");
            stringBuilder.AppendLine($"Headers");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"Content-Type:{contentType}");
            stringBuilder.AppendLine($"User-Agent:{userAgent}");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine($"Body:{body}");

            return stringBuilder.ToString();
        }

        private string GetContentType(string inputContentType)
        {
            if (inputContentType.Contains("application-json"))
                return "application-json";
            if (inputContentType.Contains("multipart/form-data"))
                return "multipart/form-data";

            return string.Empty;
        }
    }
}
