using Api.Dto.Response;
using Domain.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ExceptionBase ex)
            {
                var response = ApiResponse.Fail((int)ex.Code, ex.Message);
                var body = JsonConvert.SerializeObject(response);
                await context.Response.WriteAsync(body);
            }
        }
    }
}
