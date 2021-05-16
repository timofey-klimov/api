using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class TwoFactorAuthConfiguration : IEntityTypeConfiguration<TwoFactorAuth>
    {
        public void Configure(EntityTypeBuilder<TwoFactorAuth> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(10);

            builder.Ignore(x => x.Events);
        }
    }
}
