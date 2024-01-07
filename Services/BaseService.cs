using PSP.Models;
using PSP.Repositories;

namespace PSP.Services
{
    public abstract class BaseService<T, TCreate> : IBaseService<T, TCreate> where T : class where TCreate : class
    {
        protected readonly IBaseRepository<T> _repository;

        protected BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.FindAll();
        }

        public virtual T Get(int id)
        {
            var entity = _repository.Find(id);
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
            var entityToAdd = ModelToEntity(entity);
            return _repository.Add(entityToAdd);
        }

        public virtual T Update(TCreate creationModel, int id)
        {
            // Check if item exists
            Get(id);

            var entity = ModelToEntity(creationModel, id);
            _repository.Update(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {
            _repository.Update(entity);
            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            _repository.Remove(entity);
        }

        protected void CheckFor404(T? entity, int id)
        {
            if (entity == null)
                throw new UserFriendlyException($"Entity of type '{typeof(T).Name}' with id '{id}' was not found.", 404);
        }
    }
}
