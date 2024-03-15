using NHibernate;

namespace Lab1;

public class GroupDAO : GenericDAO<Group>, IGroupDAO
{
    public GroupDAO(ISession session) : base(session) { }
}
