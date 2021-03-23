using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Api.DependencyRegistration.Api
{
    public static class Registrate
    {
        public static IServiceCollection ApiDependecy(this IServiceCollection servces, IConfiguration configuration)
        {
            servces.AddSwaggerGen(x => 
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                });
            });

            return servces;
        }
    }
}
