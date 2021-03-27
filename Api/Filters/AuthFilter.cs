using Api.Exceptions;
using Api.Model;
using DAL;
using Logic.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbCreator = context.HttpContext.RequestServices.GetService(typeof(Func<DatabaseContext>)) as Func<DatabaseContext>;
            var jwtService = context.HttpContext.RequestServices.GetService(typeof(JwtService)) as JwtService;
            var headers = context.HttpContext
                .Request
                .Headers["Authorization"]
                .FirstOrDefault();

            if (headers == null || !headers.Contains("Bearer"))
                throw new UnauthorizedException("Unauthorized");

            var token = headers.Replace("Bearer", "")
                .Trim();

            if (!jwtService.ValidateToken(new Logic.Dto.ValidateTokenDto(token), out var userId))
                throw new TokenValidationFailedException("Token not valid");

            using(var db = dbCreator())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                    throw new UnauthorizedException("User was not found");
                var authUser = new AuthUser(user.Id, user.Login, user.Password, user.CreateDate);
                context.HttpContext.User = authUser;
            }

            await next();
        }
    }
}
