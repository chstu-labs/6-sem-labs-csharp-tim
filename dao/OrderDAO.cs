using System.Collections.Generic;
using NHibernate;

namespace Lab1;

public class OrderDAO : GenericDAO<Order>, IOrderDAO
{
    public OrderDAO(ISession session) : base(session) { }
    public IList<Order> getOrdersByWaiter(long waiterId)
    {
        var list = session.CreateSQLQuery(
        "SELECT Orders.* FROM Orders JOIN Waiters" +
        " ON Orders.WaiterId = Waiters.Id" +
        " WHERE Waiters.Id=" + waiterId)
        .AddEntity("Order", typeof(Order))
        .List<Order>();
        return list;
    }
}
