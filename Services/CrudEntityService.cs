using AutoMapper;
using PSP.Models.DTOs;
using PSP.Models.Entities;
using PSP.Models.Exceptions;
using PSP.Repositories;
using PSP.Services.Interfaces;

namespace PSP.Services
{
    public abstract class CrudEntityService<T, TCreate> : ICrudEntityService<T, TCreate> where T : class where TCreate : class
    {
        protected readonly IBaseRepository<T> _repository;
        protected readonly IMapper _mapper;

        protected CrudEntityService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var entity = Get(id);
            var newEntity = ModelToEntity(creationModel, id);
            _repository.Update(entity, newEntity);
            return newEntity;
        }

        public virtual T Update(T entity)
        {
            _repository.Update(entity);
            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            CheckFor404(entity, id);
            _repository.Remove(entity);
        }

        public void CheckFor404(int id) => CheckFor404(_repository.Find(id), id);

        public void CheckFor404(T? entity, int id) => CheckFor404(entity != null, id);

        private void CheckFor404(bool exists, int id)
        {
            if (!exists)
                throw new UserFriendlyException($"Entity of type '{typeof(T).Name}' with id '{id}' was not found.", 404);
        }
    }
}
