using NHibernate;
using NHibernate.Criterion;

namespace Lab1;

public class WaiterDAO : GenericDAO<Waiter>, IWaiterDAO
{
    public WaiterDAO(ISession session) : base(session) { }

    public IList<Waiter> GetWaitersByHeadmanName(string headmanName)
    {
        var list = session.CreateCriteria(typeof(Waiter))
            .Add(Restrictions.Like("HeadmanName", headmanName, MatchMode.Anywhere))
            .List<Waiter>();
        return list;
    }
}
