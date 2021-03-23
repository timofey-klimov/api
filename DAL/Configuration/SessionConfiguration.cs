using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Configuration
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Token)
                .HasMaxLength(400)
                .IsRequired();
            builder.Property(x => x.CreateDate)
                .HasColumnType("datetime2(3)")
                .HasDefaultValueSql("getdate()")
                .IsRequired();
            builder.Property(x => x.ExpireDate)
                .HasColumnType("datetime2(3)")
                .IsRequired();
            builder.Ignore(x => x.Events);
        }
    }
}
