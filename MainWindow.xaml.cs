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
    private GroupWindow groupWindow;
    private StudentWindow studentWindow;
    public GroupWindow getGroupWindow()
    {
        if (null == groupWindow)
        {
            groupWindow = new GroupWindow(this);
        }
        return groupWindow;
    }
    public StudentWindow getStudentWindow()
    {
        if (null == studentWindow)
        {
            studentWindow = new StudentWindow(this);
        }
        return studentWindow;

    }
    public MainWindow()
    {
        this.Closing += new CancelEventHandler(MainWindow_Closing);
        InitializeComponent();
        GroupGrid.ItemsSource = getDaoFactory().getGroupDAO().GetAll();
    }
    //Обробник натиснення на контекстне меню додання групи
    private void MenuItemAddGroup_Click(object sender, EventArgs e)
    {
        GroupWindow groupWindow = getGroupWindow();
        groupWindow.AddButton.IsEnabled = true;
        groupWindow.CancelButton.IsEnabled = true;
        groupWindow.ShowDialog();
    }
    //Обробник натиснення на контекстне меню редагування групи
    private void MenuItemEditGroup_Click(object sender, EventArgs e)
    {
        Group group = (Group)GroupGrid.SelectedItem;
        if (null == group)
        {
            MessageBox.Show("Please select group", "Nothing to edit",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            GroupWindow groupWindow = getGroupWindow();
            groupWindow.EditButton.IsEnabled = true;
            groupWindow.CancelButton.IsEnabled = true;
            groupWindow.Group = group;
            groupWindow.InputTextBox1.Text = group.GroupName;
            groupWindow.InputTextBox2.Text = group.CuratorName;
            groupWindow.InputTextBox3.Text = group.HeadmanName;
            groupWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню видалення групи
    private void MenuItemDeleteGroup_Click(object sender, EventArgs e)
    {
        Group group = (Group)GroupGrid.SelectedItem;
        if (null == group)
        {
            MessageBox.Show("Please select group", "Nothing to delete",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            getDaoFactory().getGroupDAO().Delete(group);
            GroupGrid.ItemsSource =
            getDaoFactory().getGroupDAO().GetAll();
        }
    }
    //Обробник натиснення на контекстне меню додання студента
    private void MenuItemAddStudent_Click(object sender, EventArgs e)
    {
        Group group = (Group)GroupGrid.SelectedItem;
        if (null == group)
        {
            MessageBox.Show("Please select group",
            "Student must be added to group",
            MessageBoxButton.OK, MessageBoxImage.Information,


            MessageBoxResult.No);
        }
        else
        {
            StudentWindow studentWindow = getStudentWindow();
            studentWindow.AddButton.IsEnabled = true;
            studentWindow.CancelButton.IsEnabled = true;
            studentWindow.Group = group;
            studentWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню редагування студента
    private void MenuItemEditStudent_Click(object sender, EventArgs e)
    {
        Group group = (Group)GroupGrid.SelectedItem;
        Student student = (Student)StudentGrid.SelectedItem;
        if (null == group)
        {
            MessageBox.Show("Please select student", "Nothing to edit",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            StudentWindow studentWindow = getStudentWindow();
            studentWindow.EditButton.IsEnabled = true;
            studentWindow.CancelButton.IsEnabled = true;
            studentWindow.Group = group;
            studentWindow.Student = student;
            studentWindow.InputTextBox1.Text = student.FirstName;
            studentWindow.InputTextBox2.Text = student.LastName;
            studentWindow.InputTextBox3.Text = student.Sex.ToString();
            studentWindow.InputTextBox4.Text = student.Year.ToString();
            studentWindow.ShowDialog();
        }
    }
    //Обробник натиснення на контекстне меню видалення студента
    private void MenuItemDeleteStudent_Click(object sender, EventArgs e)
    {
        Student student = (Student)StudentGrid.SelectedItem;
        if (null == student)
        {
            MessageBox.Show("Please select student", "Nothing to delete",
            MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.No);
        }
        else
        {
            student = getDaoFactory().getStudentDAO()
            .GetById(student.Id);
            student.Group.StudentList.Remove(student);
            getDaoFactory().getStudentDAO().Delete(student);
            Group group = (Group)GroupGrid.SelectedItem;
            IList<Student> studentList = getDaoFactory()
            .getStudentDAO().getStudentsByGroup(group.Id);
            StudentGrid.ItemsSource = studentList;
        }
    }
    //Обробник натиснення на рядок в таблиці групи
    private void DataGrid_Click(object sender, RoutedEventArgs e)
    {

        Group group = (Group)GroupGrid.SelectedItem;
        if (null != group)
        {
            group = getDaoFactory().getGroupDAO().GetById(group.Id);
            StudentGrid.ItemsSource = group.StudentList;
        }
    }
    //Обробник закриття головного вікна
    void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        getDaoFactory().destroy();
    }
}

