using Loyalty.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.DataAccess.EntityConfigurations
{
    public class ApiLogConfig : IEntityTypeConfiguration<APILog>
    {
        public void Configure(EntityTypeBuilder<APILog> builder)
        {
            builder.HasKey(c => c.PKID);
            builder.Property(c => c.Log).HasMaxLength(5000);
        }
    }
}
