using FluentNHibernate.Mapping;

namespace Lab1;

public class OrderMap : ClassMap<Order>
{
    public OrderMap()
    {
        Table("Orders");
        Id(x => x.Id).GeneratedBy.Native();
        Map(x => x.CustomerName);
        Map(x => x.CustomerSex);
        Map(x => x.PizzaSize);
        Map(x => x.Price);
        References(x => x.Waiter, "WaiterId");
    }
}
