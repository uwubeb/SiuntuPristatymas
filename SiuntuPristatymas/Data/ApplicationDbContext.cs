using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ParcelHistory> ParcelHistories { get; set; }

        public DbSet<Address> Addresses { get; set; }
        

    }
    
}