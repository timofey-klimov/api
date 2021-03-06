﻿using DAL.DependencyRegistration;
using Logic.DependencyRegistration;
using ApplicationSettings.DependencyRegistration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace DependencyInjection
{
    public static class Register
    {
        public static IServiceCollection RegisterAll(this IServiceCollection services, IConfiguration configuration)
        {
            services.LogicDependency(configuration)
                .DALDependency(configuration)
                .SettingsDependency(configuration)
                .InfrastructureDependency(configuration);

            return services;
        }
    }
}
