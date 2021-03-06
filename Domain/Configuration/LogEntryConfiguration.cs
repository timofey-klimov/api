using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configuration
{
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Response)
                .HasMaxLength(300)
                .IsRequired();
            builder.Property(x => x.Request)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Ignore(x => x.Events);
        }
    }
}
