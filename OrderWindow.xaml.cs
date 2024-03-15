using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
namespace Lab1;

public partial class OrderWindow : Window
{
    private MainWindow parentWindow;
    public MainWindow getParentWindow()
    {
        return parentWindow;
    }
    public Waiter Waiter { get; set; }
    public Order Order { get; set; }
    public OrderWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        this.Closing += new CancelEventHandler(Window_Closing);
        InitializeComponent();
    }
    //Обробник натиснення на кнопку додання замовлення
    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        Order order = new Order();
        order.CustomerName = InputTextBox1.Text;
        order.PizzaSize = InputTextBox2.Text;
        order.CustomerSex = InputTextBox3.Text;
        order.Price = Int32.Parse(InputTextBox4.Text);
        Waiter waiter = getParentWindow()
        .getDaoFactory().getWaiterDAO().GetById(Waiter.Id);
        order.Waiter = waiter;
        waiter.OrderList.Add(order);
        getParentWindow().getDaoFactory()
        .getOrderDAO().SaveOrUpdate(order);
        IList<Order> orderList = getParentWindow()
        .getDaoFactory().getOrderDAO()
        .getOrdersByWaiter(waiter.Id);
        getParentWindow().OrderGrid.ItemsSource = orderList;
        closeWindow();
    }
    //Обробник натиснення на кнопку редагування замовлення
    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        Order order = getParentWindow()
        .getDaoFactory().getOrderDAO().GetById(Order.Id);
        order.CustomerName = InputTextBox1.Text;
        order.PizzaSize = InputTextBox2.Text;
        order.CustomerSex = InputTextBox3.Text;
        order.Price = Int32.Parse(InputTextBox4.Text);
        getParentWindow().getDaoFactory().getOrderDAO()
        .SaveOrUpdate(order);
        getParentWindow().OrderGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getOrderDAO()
        .getOrdersByWaiter(Waiter.Id);
        closeWindow();
    }
    //Обробник натиснення на кнопку Cancel
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        closeWindow();
    }
    //Обробник закриття вікна замовлення
    private void Window_Closing(object sender,
    System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        closeWindow();
    }
    private void closeWindow()
    {
        this.AddButton.IsEnabled = false;
        this.EditButton.IsEnabled = false;
        this.CancelButton.IsEnabled = false;
        this.InputTextBox1.Text = "";
        this.InputTextBox2.Text = "";
        this.InputTextBox3.Text = "";
        this.InputTextBox4.Text = "";
        this.Hide();
    }

}