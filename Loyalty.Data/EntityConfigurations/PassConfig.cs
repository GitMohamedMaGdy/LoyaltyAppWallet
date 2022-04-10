using Loyalty.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.DataAccess.EntityConfigurations
{
    public class PassConfig : IEntityTypeConfiguration<Pass>
    {
        public void Configure(EntityTypeBuilder<Pass> modelBuilder)
        {
            modelBuilder.HasKey(c => c.PassTypeIdentifier);
            modelBuilder.HasAlternateKey(c => new { c.PassTypeIdentifier, c.SerialNumber }).HasName("IX_UniquePassIndex");
            modelBuilder.Property(c => c.PassTypeIdentifier).HasMaxLength(450);

        }
    }
}
