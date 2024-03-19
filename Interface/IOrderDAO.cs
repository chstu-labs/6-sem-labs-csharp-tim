namespace Lab4;

public interface IOrderDAO : IGenericDAO<Order>
{
    public IList<Order> SearchByCustomerName(string customerName);
}

