using ISession = NHibernate.ISession;

namespace Lab4;

public class OrderDAO : GenericDAO<Order>, IOrderDAO
{
    public OrderDAO(ISession session) : base(session) { }
}
