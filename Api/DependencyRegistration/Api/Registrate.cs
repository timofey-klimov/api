using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

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
                x.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme()
                    {
                        Description = "Jwt token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference()
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             },
                             Scheme = "oauth2",
                             Name = "Bearer",
                             In = ParameterLocation.Header
                         },
                         new List<string>()
                    }
                });
            });

            return servces;
        }
    }
}
