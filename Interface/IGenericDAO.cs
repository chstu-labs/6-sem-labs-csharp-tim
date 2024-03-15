namespace Lab4;

public interface IGenericDAO<T>
{
    T Merge(T item);
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
