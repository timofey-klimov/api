using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DependencyRegistration
{
    public static class Registrate
    {
        public static IServiceCollection DALDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DatabaseContext>>(x =>
            {
                var mediatr = x.GetRequiredService<IMediator>();
                var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
                var connstring = configuration.GetConnectionString("Database");
                var opts = optionsBuilder
                    .UseSqlServer(connstring)
                    .Options;
                return () => new DatabaseContext(opts, mediatr);
            });
            services.AddDbContext<DatabaseContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("Database"));
            });
            return services;
        }
    }
}
