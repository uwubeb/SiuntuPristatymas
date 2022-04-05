using SiuntuPristatymas.Data.Dtos;

namespace SiuntuPristatymas.Services;

public interface IParcelService
{
    Task<List<ParcelDto>> GetAll();
    Task<ParcelDto?> GetById(Guid id);
    Task Update(int id,ParcelCreateDto parcel );
    Task Delete(int id);
    Task Create(ParcelCreateDto parcel);
}