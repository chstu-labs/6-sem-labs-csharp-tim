using System;
using System.ComponentModel;
using System.Windows;
namespace Lab1;

public partial class GroupWindow : Window
{
    private MainWindow parentWindow;
    public Group Group { get; set; }
    public GroupWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        this.Closing += new CancelEventHandler(Window_Closing);
        InitializeComponent();
    }
    public MainWindow getParentWindow()
    {
        return parentWindow;
    }
    //Обробник натиснення на кнопку додання групи
    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        Group group = new Group();
        group.GroupName = InputTextBox1.Text;
        group.CuratorName = InputTextBox2.Text;
        group.HeadmanName = InputTextBox3.Text;
        getParentWindow().getDaoFactory()
        .getGroupDAO().SaveOrUpdate(group);
        getParentWindow().GroupGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getGroupDAO().GetAll();
        closeWindow();
    }
    //Обробник натиснення на кнопку редагування групи
    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        Group group = getParentWindow()
        .getDaoFactory().getGroupDAO().GetById(Group.Id);
        group.GroupName = InputTextBox1.Text;
        group.CuratorName = InputTextBox2.Text;
        group.HeadmanName = InputTextBox3.Text;

        getParentWindow().getDaoFactory().getGroupDAO()
        .SaveOrUpdate(group);
        getParentWindow().GroupGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getGroupDAO().GetAll();
        closeWindow();
    }
    //Обробник натиснення на кнопку Cancel
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        closeWindow();
    }
    //Обробник закриття вікна групи
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
