using System.Collections.Generic;

namespace MvcTraining.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll(string searchParam, string pagination);
        T GetById(long id);
        int Create(T item);
        int ListCount();
        int FilterListCount(string searchParam);
        int DuplicateCreate(T item);
        int Update(T item);
        int DuplicateUpdate(T item);
        int Delete(long id);
    }
}
