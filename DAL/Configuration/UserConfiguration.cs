using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Login)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.Password)
                .HasColumnType("varbinary(max)")
                .IsRequired();
            builder.Ignore(x => x.Events);
            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(3)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();
            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(3)");
        }
    }
}
