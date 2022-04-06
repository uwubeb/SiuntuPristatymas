using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Siuntos.Data.Models;

namespace SiuntuPristatymas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ParcelHistory> ParcelHistories { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}