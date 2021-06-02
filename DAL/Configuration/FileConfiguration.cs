using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(200);
            builder.Property(x => x.FilePath)
                .HasMaxLength(200);
            builder.Property(x => x.HashCode)
                .HasColumnType("varbinary(max)");
            builder.ToTable("Files");
            builder.Ignore(x => x.Events);
        }
    }
}
