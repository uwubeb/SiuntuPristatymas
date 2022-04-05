using System.Linq.Expressions;
using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetById(int id);
    Task<List<TEntity>> GetAll();

    Task<TEntity> Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    
    Task<List<TEntity>> GetMany(List<int> ids);
    Task<List<TEntity>> CreateMany(List<TEntity> entities);
    Task UpdateMany(List<TEntity> entities);
    Task DeleteMany(List<TEntity> entities);

    Task<int> Count(Expression<Func<TEntity, bool>>? expression = null);
    Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
    
    IQueryable<TEntity> GetQueryable();
    Task<TProjection?> GetProjection<TProjection>(int id, Expression<Func<TEntity, TProjection>> projectionExpression);
}