using System.Collections.Generic;

namespace Lab1;

public interface IGenericDAO<T>
{
    T Merge(T item);
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
    T GetPersistentObject(T nonPersistentObject);
}
