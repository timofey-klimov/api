using DAL.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        private readonly IMediator _mediator;
        public DatabaseContext(DbContextOptions<DatabaseContext> opts, IMediator mediator)
            : base(opts)
        {
            _mediator = mediator;
            Database.Migrate();
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> opts)
            :base(opts)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<LogEntry> LogEntries { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }

        public DbSet<TwoFactorAuth> TwoFactorAuths { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public int SaveChanges()
        {
            return this.SaveChangesAsync()
                    .GetAwaiter()
                    .GetResult();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var entities = this.ChangeTracker
                    .Entries()
                    .Select(x => x.Entity)
                    .Cast<BaseNotifyEntity>()
                    .ToList();

                var @events = entities
                    .SelectMany(x => x.Events);

                entities
                    .ToList()
                    .ForEach(x => x.ClearEvents());

                var result = await base.SaveChangesAsync();

                events
                    .ToList()
                    .ForEach(async x => await _mediator.Publish(x, cancellationToken));

                return result;
            }
            catch(Exception ex)
            {
                throw new DbSaveChangesException("Ошибка при сохранение данных");
            }
        }
    }
}
