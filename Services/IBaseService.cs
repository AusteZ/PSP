using System;
using System.Collections.Generic;

namespace PSP.Services
{
    public interface IBaseService<T, TCreate> where T : class where TCreate : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(TCreate entity);
        T Update(TCreate creationModel, int id);
        T Update(T entity);
        void Delete(int id);
    }
}