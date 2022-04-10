using Loyalty.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.DataAccess.EntityConfigurations
{
    public class DeviceConfig : IEntityTypeConfiguration<Device>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Device> modelBuilder)
        {
            modelBuilder.HasKey(c => c.DeviceLibraryIdentifier);
            modelBuilder.Property(c => c.DeviceLibraryIdentifier).HasMaxLength(450);
        }
    }

}
