using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Lab1;

public partial class MainWindow : Window
{
    private DAOFactory daoFactory;
    public DAOFactory getDaoFactory()
    {
        if (null == daoFactory)
        {
            daoFactory = NHibernateDAOFactory.getInstance();
        }
        return daoFactory;
    }

    private WaiterWindow waiterWindow;
    private OrderWindow orderWindow;

    public WaiterWindow getWaiterWindow()
    {
        if (null == waiterWindow)
        {
            waiterWindow = new WaiterWindow(this);
        }
        return waiterWindow;
    }

    public OrderWindow getOrderWindow()
    {
        if (null == orderWindow)
        {
            orderWindow = new OrderWindow(this);
        }
        return orderWindow;
    }


    public MainWindow()
    {
        this.Closing += new CancelEventHandler(MainWindow_Closing);
        InitializeComponent();
        WaiterGrid.ItemsSource = getDaoFactory().getWaiterDAO().GetAll();
    }

    //Обробник натиснення на контекстне меню додання офіціанта
    private void MenuItemAddWaiter_Click(object sender, EventArgs e)
    {
        WaiterWindow waiterWindow = getWaiterWindow();
        waiterWindow.AddButton.IsEnabled = true;
        waiterWindow.CancelButton.IsEnabled = true;
        waiterWindow.ShowDialog();
    }

    //Обробник натиснення на контекстне меню редагування офіціанта
    private void MenuItemEditWaiter_Click(object sender, EventArgs e)
    {
        Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
        if (null == waiter)
        {
            MessageBox.Show("Please select waiter", "Nothing to edit",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            WaiterWindow waiterWindow = getWaiterWindow();
            waiterWindow.EditButton.IsEnabled = true;
            waiterWindow.CancelButton.IsEnabled = true;
            waiterWindow.Waiter = waiter;
            waiterWindow.InputTextBox1.Text = waiter.FirstName;
            waiterWindow.InputTextBox2.Text = waiter.LastName;
            waiterWindow.InputTextBox3.Text = waiter.HeadmanName;
            waiterWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню видалення офіціанта
    private void MenuItemDeleteWaiter_Click(object sender, EventArgs e)
    {
        Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
        if (null == waiter)
        {
            MessageBox.Show("Please select waiter", "Nothing to delete",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            getDaoFactory().getWaiterDAO().Delete(waiter);
            WaiterGrid.ItemsSource = getDaoFactory().getWaiterDAO().GetAll();
        }
    }

    //Обробник натиснення на контекстне меню додання замовлення
    private void MenuItemAddOrder_Click(object sender, EventArgs e)
    {
        Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
        if (null == waiter)
        {
            MessageBox.Show("Please select waiter",
            "Order must be added to waiter",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            OrderWindow orderWindow = getOrderWindow();
            orderWindow.AddButton.IsEnabled = true;
            orderWindow.CancelButton.IsEnabled = true;
            orderWindow.Waiter = waiter;
            orderWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню редагування замовлення
    private void MenuItemEditOrder_Click(object sender, EventArgs e)
    {
        Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
        Order order = (Order)OrderGrid.SelectedItem;
        if (null == waiter)
        {
            MessageBox.Show("Please select waiter",
            "Order must be added to waiter",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else if (null == order)
        {
            MessageBox.Show("Please select order", "Nothing to edit",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            OrderWindow orderWindow = getOrderWindow();
            orderWindow.EditButton.IsEnabled = true;
            orderWindow.CancelButton.IsEnabled = true;
            orderWindow.Waiter = waiter;
            orderWindow.Order = order;
            orderWindow.InputTextBox1.Text = order.CustomerName;
            orderWindow.InputTextBox2.Text = order.PizzaSize;
            orderWindow.InputTextBox3.Text = order.CustomerSex;
            orderWindow.InputTextBox4.Text = order.Price.ToString();
            orderWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню видалення замовлення
    private void MenuItemDeleteOrder_Click(object sender, EventArgs e)
    {
        Order order = (Order)OrderGrid.SelectedItem;
        if (null == order)
        {
            MessageBox.Show("Please select order", "Nothing to delete",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
            order = getDaoFactory().getOrderDAO().GetById(order.Id);
            waiter.OrderList.Remove(order);
            getDaoFactory().getOrderDAO().Delete(order);
            IList<Order> orderList = getDaoFactory().getOrderDAO()
            .getOrdersByWaiter(waiter.Id);
            OrderGrid.ItemsSource = orderList;
        }
    }

    private void ButtonSearch_Click(object sender, RoutedEventArgs e)
    {
        string headmanName = SearchTextBox.Text;
        IList<Waiter> waiters = getDaoFactory().getWaiterDAO().GetWaitersByHeadmanName(headmanName);
        WaiterGrid.ItemsSource = waiters;
        if (waiters.Count > 0)
        {
            WaiterGrid.SelectedItem = waiters[0];
        }
    }

    //Обробник натиснення на рядок в таблиці офіціантів
    private void DataGrid_Click(object sender, RoutedEventArgs e)
    {
        Waiter waiter = (Waiter)WaiterGrid.SelectedItem;
        if (null != waiter)
        {
            waiter = getDaoFactory().getWaiterDAO().GetById(waiter.Id);
            OrderGrid.ItemsSource = waiter.OrderList;
        }
    }


    //Обробник закриття головного вікна
    void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        getDaoFactory().destroy();
    }
}

