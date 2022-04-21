using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Repositories;

public interface IQueryRepository<TEntity>  : IRepository<TEntity> where TEntity : BaseEntity
{
    Task<List<TEntity>> GetAll(string searchString);
    // TEntity GetById(int id);
}