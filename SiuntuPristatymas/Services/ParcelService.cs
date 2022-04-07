using SiuntuPristatymas.Data.Dtos;

namespace SiuntuPristatymas.Services;

public class ParcelService : IParcelService
{

    public Task<List<ParcelDto>> GetAll()
    {
            throw new NotImplementedException();
    }
    public Task<ParcelDto?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task Update(int id, ParcelCreateDto parcel)
    {
        throw new NotImplementedException();
    }
    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
    public Task Create(ParcelCreateDto parcel)
    {
        throw new NotImplementedException();
    }
}