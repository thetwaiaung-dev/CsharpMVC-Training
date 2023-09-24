using System.Collections.Generic;

namespace MVCTraining.DBHelper
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetOne(long id);
        int Create(T item);
        int DuplicateCreate(T item);
        int Update(long id, T item);
        int DuplicateUpdate(T item);
        int Delete(long id);
    }
}
