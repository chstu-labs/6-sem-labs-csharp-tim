using NHibernate;

namespace Lab1;

public class WaiterDAO : GenericDAO<Group>, IWaiterDAO
{
    public WaiterDAO(ISession session) : base(session) { }
}
