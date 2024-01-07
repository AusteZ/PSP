using Microsoft.EntityFrameworkCore;
using PSP.Models;

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

    public virtual T? Find(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual T Add(T entity)
    {
        var addedEntity = _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return addedEntity.Entity;
    }

    public virtual T Update(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }
}