using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IList<Order> orderList = OrderRepository.GetInstance().GetAll();
            OrderGrid.ItemsSource = orderList;
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            Order order = (Order)OrderGrid.SelectedItem;
            InputTextBox1.Text = order.Id.ToString();
            InputTextBox2.Text = order.CustomerName;
            InputTextBox3.Text = order.PizzaSize;
            ComboBox1.Text = order.CustomerSex;
            InputTextBox4.Text = order.Price.ToString();
            AddButton.IsEnabled = false;
            EditButton.IsEnabled = true;
            CancelButton.IsEnabled = true;
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            string messageBoxText = "Do you want to delete this order?";
            string caption = "Delete order";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result.Equals(MessageBoxResult.OK))
            {
                Order order = (Order)OrderGrid.SelectedItem;
                OrderRepository.GetInstance().Delete(order.Id);
                IList<Order> orderList = OrderRepository.GetInstance().GetAll();
                OrderGrid.ItemsSource = orderList;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.CustomerName = InputTextBox2.Text;
            order.PizzaSize = InputTextBox3.Text;
            order.CustomerSex = ComboBox1.Text;
            order.Price = Convert.ToInt32(InputTextBox4.Text);
            OrderRepository.GetInstance().Save(order);
            IList<Order> orderList = OrderRepository.GetInstance().GetAll();
            OrderGrid.ItemsSource = orderList;
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.Id = Convert.ToInt64(InputTextBox1.Text);
            order.CustomerName = InputTextBox2.Text;
            order.PizzaSize = InputTextBox3.Text;
            order.CustomerSex = ComboBox1.Text;
            order.Price = Convert.ToInt32(InputTextBox4.Text);
            OrderRepository.GetInstance().Update(order);
            IList<Order> orderList = OrderRepository.GetInstance().GetAll();
            OrderGrid.ItemsSource = orderList;
            ClearFields();
            EditButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            AddButton.IsEnabled = true;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            EditButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
            AddButton.IsEnabled = true;
        }

        private void ClearFields()
        {
            InputTextBox1.Text = "";
            InputTextBox2.Text = "";
            InputTextBox3.Text = "";
            ComboBox1.Text = "";
            InputTextBox4.Text = "";
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            OrderRepository.GetInstance().Destroy();
        }
    }
}
