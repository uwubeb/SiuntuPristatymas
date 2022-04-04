using SiuntuPristatymas.Data;

namespace SiuntuPristatymas.Repositories;

public class ParcelRepository : BaseRepository<Parcel>
{
    public ParcelRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        ItemSet = dbContext.Parcels;
    }
}