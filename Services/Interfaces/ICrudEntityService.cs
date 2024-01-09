using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace PSP.Services.Interfaces
{
    public interface ICrudEntityService<T, TCreate> where T : class where TCreate : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(TCreate entity);
        T Update(TCreate creationModel, int id);
        T Update(T entity);
        T Patch(JsonPatchDocument<T> patchDocument, int id);
        void Delete(int id);
    }
}