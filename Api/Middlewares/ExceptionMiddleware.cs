using Api.Dto.Response;
using Domain.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            try
            {
                await _next(context);
            }
            catch(ExceptionBase ex)
            {
                logger.Error(ex.Message);
                context.Response.ContentType = "application/json";
                var response = ApiResponse.Fail(ex.Code.To<int>(), ex.Message);
                var body = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(body);
            }
        }
    }
}
