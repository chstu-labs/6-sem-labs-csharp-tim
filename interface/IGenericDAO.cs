using System.Collections.Generic;

namespace Lab1;

public interface IGenericDAO<T>
{
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
