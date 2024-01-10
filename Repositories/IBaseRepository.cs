namespace PSP.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        T? Find(params int[] ids);
        T Add(T entity);
        T Update(T entity, T newEntity);
        T Update(T entity);
        void Remove(T entity);
        IQueryable<T> GetQueryable();
    }
}