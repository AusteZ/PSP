using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities;

namespace PSP.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(PSPDatabaseContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }

    public virtual IEnumerable<T> FindAll()
    {
        return _dbSet.ToList();
    }

    public virtual T? Find(params int[] ids)
    {
        return _dbSet.Find(ids[0]);
    }

    public virtual T Add(T entity)
    {
        var addedEntity = _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public virtual T Update(T entity, T newEntity)
    {
        _dbContext.Entry(entity).CurrentValues.SetValues(newEntity);
        _dbContext.SaveChanges();
        return entity;
    }

    public virtual T Update(T entity)
    {
        var addedEntity = _dbSet.Update(entity);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public virtual IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}