using Microsoft.AspNetCore.Mvc;

namespace Lab4;

public class OrderController : Controller
{
    //Обробник виводу головної сторінки
    public IActionResult GetAll()
    {
        List<Order> orderList = NHibernateDAOFactory
            .getInstance().getOrderDAO().GetTop10().ToList();
        return View(orderList);
    }
    //Обробник додання студента
    [HttpPost]
    public IActionResult Add(
    [Bind("CustomerName, PizzaSize, CustomerSex, Price")] Order order)
    {
        NHibernateDAOFactory.getInstance()
        .getOrderDAO().SaveOrUpdate(order);
        return RedirectToAction("GetAll");
    }
    //Обробник відкриття форми редагування студента
    [Route("EditForm/{id}")]
    public IActionResult EditForm(long id)
    {
        Order order = NHibernateDAOFactory
        .getInstance().getOrderDAO().GetById(id);
        return View(order);
    }
    //Обробник редагування студента
    [HttpPost]
    public IActionResult Edit(
    [Bind("Id, CustomerName, PizzaSize, CustomerSex, Price")] Order order)
    {
        Order orderToUpdate = NHibernateDAOFactory
        .getInstance().getOrderDAO().GetById(order.Id);
        orderToUpdate.CustomerName = order.CustomerName;
        orderToUpdate.PizzaSize = order.PizzaSize;
        orderToUpdate.CustomerSex = order.CustomerSex;
        orderToUpdate.Price = order.Price;
        NHibernateDAOFactory.getInstance()
        .getOrderDAO().SaveOrUpdate(orderToUpdate);
        return RedirectToAction("GetAll");
    }
    //Обробник видалення студента
    [Route("Delete/{id}")]
    public IActionResult Delete(long id)
    {
        Order order = NHibernateDAOFactory
        .getInstance().getOrderDAO().GetById(id);
        NHibernateDAOFactory.getInstance()
        .getOrderDAO().Delete(order);
        return RedirectToAction("GetAll");
    }
}