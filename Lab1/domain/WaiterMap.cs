using FluentNHibernate.Mapping;

namespace Lab1;

public class WaiterMap : ClassMap<Waiter>
{
    public WaiterMap()
    {
        Table("Waiters");
        Id(x => x.Id).GeneratedBy.Native();
        Map(x => x.FirstName);
        Map(x => x.LastName);
        Map(x => x.HeadmanName);
        HasMany(x => x.OrderList)
        .Cascade.All()
        .KeyColumn("OrderId");
    }
}

