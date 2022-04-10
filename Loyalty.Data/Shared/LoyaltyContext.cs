using Loyalty.DataAccess.EntityConfigurations;
using Loyalty.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Loyalty.DataAccess.Shared
{
    public class LoyaltyContext : DbContext
    {
        public LoyaltyContext(DbContextOptions<LoyaltyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceConfig());
            modelBuilder.ApplyConfiguration(new PassConfig());
            modelBuilder.ApplyConfiguration(new RegisterationConfig());
            modelBuilder.ApplyConfiguration(new ApiLogConfig());
        }
        public DbSet<Pass> Passes { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<APILog> Logs { get; set; }
    }
}
