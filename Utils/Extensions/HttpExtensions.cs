using Microsoft.AspNetCore.Http;
using System.IO;

namespace Utils.Extensions
{
    public static class HttpExtensions
    {
        public static string GetRequestBody(this HttpContext context)
        {
            return new StreamReader(context.Request.Body).ReadToEnd();
        }
    }
}
