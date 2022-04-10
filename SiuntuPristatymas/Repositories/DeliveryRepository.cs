using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery>
    {
        public DeliveryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            ItemSet = dbContext.Deliveries;
        }
        public override async Task<List<Delivery>> GetAll()
        {
            var deliveries = ItemSet.Include(x => x.Car).Include(x => x.DeliveryRoute).Include(x => x.Parcels).ToListAsync();

            return await deliveries;
        }
        
        public override async Task<Delivery> Create(Delivery delivery)
        {
           
            var entry = await ItemSet.AddAsync(delivery);
            await entry.Context.SaveChangesAsync();
            return delivery;

        }

    }
}
