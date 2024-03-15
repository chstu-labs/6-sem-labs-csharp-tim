using NHibernate;

namespace Lab1;

public class WaiterDAO : GenericDAO<Waiter>, IWaiterDAO
{
    public WaiterDAO(ISession session) : base(session) { }
}
