

using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SiuntuPristatymas.Data;
using SiuntuPristatymas.Data.Base;

namespace SiuntuPristatymas.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    protected DbSet<TEntity> ItemSet;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> GetById(int id)
    {
        return await ItemSet.FirstOrDefaultAsync(e => e.Id == id);
    }
    public virtual async Task<List<TEntity>> GetAll()
    {
        return await ItemSet.ToListAsync();
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        await ItemSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public virtual async Task Update(TEntity entity)
    {
        ItemSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task Delete(TEntity entity)
    {
        ItemSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> GetMany(List<int> ids)
    {
        return await ItemSet.Where(e => ids.Any(id => id == e.Id)).ToListAsync();
    }

    public virtual async Task<List<TEntity>> CreateMany(List<TEntity> entities)
    {
        await ItemSet.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        return entities;
    }

    public virtual async Task UpdateMany(List<TEntity> entities)
    {
        ItemSet.UpdateRange(entities);
        await _dbContext.SaveChangesAsync();

    }

    public virtual async Task DeleteMany(List<TEntity> entities)
    {
        ItemSet.RemoveRange(entities);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<int> Count(Expression<Func<TEntity, bool>>? expression = null)
    {
        var count = expression == null ? await ItemSet.CountAsync() : await ItemSet.CountAsync(expression);

        return count;
    }

    public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
    {
        return await ItemSet.AnyAsync(expression);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return ItemSet;
    }

    public virtual async Task<TProjection?> GetProjection<TProjection>(int id, Expression<Func<TEntity, TProjection>> projectionExpression)
    {
        return await ItemSet
            .Where(e => e.Id == id)
            .Select(projectionExpression)
            .FirstOrDefaultAsync();
    }
}
