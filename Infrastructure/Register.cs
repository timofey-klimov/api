using ApplicationSettings.Settings;
using Infrastructure.MailClient;
using Logic.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Register
    {
        public static void RegisterInfrastructure(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddScoped<IMailSenderClient, MailSenderClient>();
        }
    }
}
