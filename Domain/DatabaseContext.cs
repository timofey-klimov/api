using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class DatabaseContext : DbContext
    {
        private readonly IMediator _mediator;
        public DatabaseContext(DbContextOptions<DatabaseContext> opts, IMediator mediator)
            :base(opts)
        {
            _mediator = mediator;
        }

        public DatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public new void SaveChanges()
        {
            this.SaveChangesAsync()
                .Wait();
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = this.ChangeTracker
                .Entries()
                .Cast<BaseNotifyEntity>();

            var @events = entities
                .SelectMany(x => x.Events);

            entities
                .ToList()
                .ForEach(x => x.ClearEvents());

            var result = await base.SaveChangesAsync();

            events
                .ToList()
                .ForEach(x => _mediator.Publish(x, cancellationToken));

            return result;
        }
    }
}
