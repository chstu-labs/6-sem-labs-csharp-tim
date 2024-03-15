namespace Lab4;

public interface IGenericDAO<T>
{
    void SaveOrUpdate(T item);
    T GetById(long id);
    List<T> GetAll();
    void Delete(T item);
}
