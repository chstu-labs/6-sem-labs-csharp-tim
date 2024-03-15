using Microsoft.AspNetCore.Mvc;

namespace Lab4;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase
{
    //Сервіс для виводу всіх студентів
    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    public IList<Order> GetAllOrders()
    {
        IList<Order> orders = NHibernateDAOFactory
        .getInstance().getOrderDAO().GetAll();
        return orders;
    }
    //Сервіс для додавання нового студента
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    public Order AddOrder(Order order)
    {
        Order resultOrder = NHibernateDAOFactory
        .getInstance().getOrderDAO().Merge(order);
        return resultOrder;
    }
    //Сервіс для редагування студента
    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    public Order UpdateOrder(Order order)
    {

        Order resultOrder = NHibernateDAOFactory
 .getInstance().getOrderDAO().Merge(order);
        return resultOrder;
    }
    //Сервіс для видалення студента
    [HttpDelete]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("{id}")]
    public string Delete(long id)
    {
        string result;
        Order order = NHibernateDAOFactory
        .getInstance().getOrderDAO().GetById(id);
        if (null != order)
        {
            NHibernateDAOFactory.getInstance()
            .getOrderDAO().Delete(order);
            result = "Order was successfully removed.";
        }
        else
        {
            result = "Nothing was removed.";
        }
        return result;
    }
}
