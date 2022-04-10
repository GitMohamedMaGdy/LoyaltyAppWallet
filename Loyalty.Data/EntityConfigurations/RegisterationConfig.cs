using Loyalty.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.DataAccess.EntityConfigurations
{
    public class RegisterationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Registration> modelBuilder)
        {
            modelBuilder.HasKey(c => new { c.DeviceLibraryIdentifier, c.PassTypeIdentifier });
            modelBuilder.HasOne(c => c.pass).WithMany(b => b.Registrations)
                .HasForeignKey(c => c.PassTypeIdentifier).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasOne(c => c.Device).WithMany(b => b.Registrations)
                .HasForeignKey(c => c.DeviceLibraryIdentifier).OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.Property(c => c.SerialNumber).IsRequired();
            modelBuilder.Property(c => c.PushToken).IsRequired();
            modelBuilder.Property(c => c.DeviceLibraryIdentifier).IsRequired().HasMaxLength(450);
            modelBuilder.Property(c => c.PassTypeIdentifier).IsRequired().HasMaxLength(450);
        }
    }
}
