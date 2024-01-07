namespace PSP.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        T? Find(int id);
        T Add(T entity);
        T Update(T entity);
        void Remove(T entity);
        IQueryable<T> GetQueryable();
    }
}