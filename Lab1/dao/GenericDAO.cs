using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using ISession = NHibernate.ISession;
namespace Lab1;

public class GenericDAO<T> : IGenericDAO<T>
{
    protected ISession session;
    public GenericDAO() { }

    public GenericDAO(ISession session)
    {
        this.session = session;
    }
    public void SaveOrUpdate(T item)
    {
        ITransaction transaction = session.BeginTransaction();
        session.SaveOrUpdate(item);
        transaction.Commit();
    }

    public T Merge(T item)
    {
        ITransaction transaction = session.BeginTransaction();
        T resultItem = (T)session.Merge(item);
        transaction.Commit();
        return resultItem;
    }

    public T GetById(long id)
    {
        return session.Get<T>(id);
    }
    public List<T> GetAll()
    {
        return new List<T>(session.CreateCriteria(typeof(T)).List<T>());
    }
    public void Delete(T item)
    {
        ITransaction transaction = session.BeginTransaction();
        session.Delete(item);
        transaction.Commit();
    }

    public T GetPersistentObject(T nonPersistentObject)
    {
        ICriteria criteria = session.CreateCriteria(typeof(T))
        .Add(Example.Create(nonPersistentObject));
        IList<T> list = criteria.List<T>();
        return list[0];
    }
}
