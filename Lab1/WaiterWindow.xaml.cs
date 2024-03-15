using System;
using System.ComponentModel;
using System.Windows;
namespace Lab1;

public partial class WaiterWindow : Window
{
    private MainWindow parentWindow;
    public Waiter Waiter { get; set; }
    public WaiterWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        this.Closing += new CancelEventHandler(Window_Closing);
        InitializeComponent();
    }
    public MainWindow getParentWindow()
    {
        return parentWindow;
    }
    //Обробник натиснення на кнопку додання офіціанта
    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        Waiter waiter = new Waiter();
        waiter.FirstName = InputTextBox1.Text;
        waiter.LastName = InputTextBox2.Text;
        waiter.HeadmanName = InputTextBox3.Text;
        getParentWindow().getDaoFactory()
        .getWaiterDAO().SaveOrUpdate(waiter);
        getParentWindow().WaiterGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getWaiterDAO().GetAll();
        closeWindow();
    }
    //Обробник натиснення на кнопку редагування офіціанта
    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        Waiter waiter = getParentWindow()
        .getDaoFactory().getWaiterDAO().GetById(Waiter.Id);
        waiter.FirstName = InputTextBox1.Text;
        waiter.LastName = InputTextBox2.Text;
        waiter.HeadmanName = InputTextBox3.Text;

        getParentWindow().getDaoFactory().getWaiterDAO()
        .SaveOrUpdate(waiter);
        getParentWindow().WaiterGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getWaiterDAO().GetAll();
        closeWindow();
    }
    
    //Обробник натиснення на кнопку Cancel
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        closeWindow();
    }
    //Обробник закриття вікна студента
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
        this.Hide();
    }
}
