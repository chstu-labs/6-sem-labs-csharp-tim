using ISession = NHibernate.ISession;

namespace Lab4;

public class OrderDAO : GenericDAO<Order>, IOrderDAO
{
    public OrderDAO(ISession session) : base(session) { }

    public IList<Order> GetTop10()
    {
        var orders = session.CreateCriteria(typeof(Order))
            .AddOrder(NHibernate.Criterion.Order.Desc("Price"))
            .SetMaxResults(10)
            .List<Order>();
        return orders;
    }
}
