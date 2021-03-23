using Logic.Services;
using Logic.Services.Base;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Logic.DependencyRegistration
{
    public static class Registrate
    {
        public static IServiceCollection LogicDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<JwtService>();

            return services;
        }
    }
}
