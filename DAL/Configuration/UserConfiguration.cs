using Domain.Models;
using Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Login, a =>
            {
                a.Property(u => u.Value)
                    .HasColumnName("Login")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Email, a =>
            {
                a.Property(u => u.Value)
                    .HasColumnName("Login")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Password, a =>
            {
                a.Property(u => u.Value)
                    .HasColumnName("Password")
                    .HasColumnType("varbinary(max)")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.PhoneNumber, a =>
            {
                a.Property(u => u.Value)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("nvarchar(20)");
            });

            builder.Ignore(x => x.Events);

            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(3)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder.Property(x => x.UpdateDate)
                .HasColumnType("datetime2(3)");

            builder.HasMany(x => x.Files)
                .WithOne(x => x.User)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
