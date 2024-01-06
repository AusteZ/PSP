using Microsoft.EntityFrameworkCore;
using PSP.Models;

namespace PSP.Services
{
    public abstract class BaseService<T, TCreate> where T : class where TCreate : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        protected BaseService(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T Get(int id)
        {
            var entity = _dbSet.Find(id);
            CheckFor404(entity, id);
            return entity!;
        }

        /// <summary>
        /// Converts creation model like <see cref="ServiceCreate"/> to an actual entity with an Id like <see cref="Service"/>.
        /// </summary>
        /// <param name="creationModel">The properties of the entity that will be created.</param>
        /// <param name="id">The id of the entity, only used when modifying entities.</param>
        /// <returns>An entity that will be added to the database or modified.</returns>
        protected abstract T ModelToEntity(TCreate creationModel, int id = 0);

        public virtual T Add(TCreate entity)
        {
            var addedEntity = _dbSet.Add(ModelToEntity(entity));
            _dbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public virtual T Update(TCreate creationModel, int id)
        {
            // Check if item exists
            Get(id);

            var entity = ModelToEntity(creationModel, id);
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        protected void CheckFor404(T? entity, int id)
        {
            if (entity == null)
                throw new UserFriendlyException($"Entity of type '{typeof(T).Name}' with id '{id}' was not found.", 404);
        }
    }
}
