using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Ignore(x => x.Events);
            builder.Property(x => x.Extension)
                .HasMaxLength(20);
            builder.ToTable("Images");
        }
    }
}
