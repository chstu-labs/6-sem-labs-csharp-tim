using System.Xml.Serialization;
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
    [HttpGet("search")]
    [Produces("application/xml")]
    public IActionResult SearchByCustomerName([FromQuery] string customerName)
    {
        var orders = NHibernateDAOFactory.getInstance().getOrderDAO().SearchByCustomerName(customerName);
        var serializer = new XmlSerializer(typeof(List<Order>));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, orders);
        return Content(stringWriter.ToString(), "application/xml");
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
