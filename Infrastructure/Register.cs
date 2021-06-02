using ApplicationSettings.Settings;
using Infrastructure.FileShare;
using Infrastructure.MailClient;
using Logic.Abstract;
using Logic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Register
    {
        public static IServiceCollection InfrastructureDependency(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddScoped<IMailSenderClient, MailSenderClient>();
            collection.AddScoped<IFileShareImagesService, LocalFileShareImagesService>();

            return collection;
        }
    }
}
