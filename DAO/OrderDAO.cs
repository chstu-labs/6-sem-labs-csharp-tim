using ISession = NHibernate.ISession;

namespace Lab4;

public class OrderDAO : GenericDAO<Order>, IOrderDAO
{
    public OrderDAO(ISession session) : base(session) { }

    public IList<Order> SearchByCustomerName(string customerName)
    {
        var sql = "SELECT * FROM orders WHERE customername LIKE :customerName";
        var query = session.CreateSQLQuery(sql)
                          .AddEntity(typeof(Order))
                          .SetParameter("customerName", $"%{customerName}%");
        var orders = query.List<Order>();
        return orders;
    }
}
