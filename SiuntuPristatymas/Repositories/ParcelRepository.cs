using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Repositories;

public class ParcelRepository : BaseRepository<Parcel>
{
    public ParcelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        ItemSet = dbContext.Parcels;
    }

    public override async Task<List<Parcel>> GetAll()
    {
        var parcels = ItemSet.Include(p => p.Address).Include(p => p.Delivery).ToListAsync();

        return await parcels;
        // return base.GetAll();
    }

    public override async Task<Parcel?> GetById(int id)
    {
        var parcel = await ItemSet
            .Include(p => p.Address)
            .Include(p => p.Delivery)
            .Include(p => p.ParcelHistory)
            .FirstOrDefaultAsync(m => m.Id == id);
        return parcel;
    }
}