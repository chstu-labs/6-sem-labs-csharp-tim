using System.Collections.Generic;
namespace Lab1;

public interface IOrderDAO : IGenericDAO<Order>
{
    IList<Order> getOrdersByWaiter(long waiterId);
    public List<Order> GetTop10OrdersByPrice();
}