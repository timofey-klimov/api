using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Response)
                .HasMaxLength(1500)
                .IsRequired();
            builder.Property(x => x.Request)
                .HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(3)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();
            builder.Ignore(x => x.Events);
        }
    }
}
