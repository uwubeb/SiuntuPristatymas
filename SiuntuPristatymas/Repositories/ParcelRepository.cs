using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Models;

namespace SiuntuPristatymas.Repositories;

public class ParcelRepository : BaseRepository<Parcel>
{
    public ParcelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        ItemSet = dbContext.Parcels;
    }
}