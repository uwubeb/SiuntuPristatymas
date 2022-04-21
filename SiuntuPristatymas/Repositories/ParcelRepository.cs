using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Repositories;

public class ParcelRepository : BaseRepository<Parcel>, IQueryRepository<Parcel>
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
    
    public async Task<List<Parcel>> GetAll(string searchString)
    {
        if (String.IsNullOrEmpty(searchString))
        {
            var parcels = ItemSet.Include(p => p.Address).Include(p => p.Delivery).ToListAsync();
            return await parcels;
        }
        else
        {
            searchString = searchString.ToLower();
            //check if any property of the object matches the search string
            var parcels = ItemSet
                .Include(p => p.Address)
                .Include(p => p.Delivery)
                .Where(p => p.Length.ToString().Contains(searchString) 
                            || p.Width.ToString().Contains(searchString) 
                            || p.Height.ToString().Contains(searchString)
                            //ParcelStatusEnum get description
                            // || p.Status.GetDescription().ToLower().Contains(searchString)
                            // || p.Status.GetDescription<ParcelStatusEnum>().Contains(searchString)
                            // gotta figure out this
                            || p.Delivery.Id.ToString().Contains(searchString) 
                            || p.AddressId.ToString().Contains(searchString)) 
                .ToListAsync();
            return await parcels;
        }

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