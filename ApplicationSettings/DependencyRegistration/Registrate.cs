using ApplicationSettings.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSettings.DependencyRegistration
{
    public static class Registrate
    {
        public static IServiceCollection SettingsDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            return services;
        }
    }
}
