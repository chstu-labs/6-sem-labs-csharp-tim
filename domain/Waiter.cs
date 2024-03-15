using System.Collections.Generic;
namespace Lab1;
//Доменний клас групи

public class Waiter : EntityBase
{
    private IList<Order> orderList = new List<Order>();
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string HeadmanName { get; set; }
    public virtual IList<Order> OrderList
    {
        get { return orderList; }
        set { orderList = value; }
    }
}
