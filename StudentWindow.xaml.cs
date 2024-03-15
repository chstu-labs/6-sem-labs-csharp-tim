using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
namespace Lab1;

public partial class StudentWindow : Window
{
    private MainWindow parentWindow;
    public MainWindow getParentWindow()
    {
        return parentWindow;
    }
    public Group Group { get; set; }
    public Student Student { get; set; }
    public StudentWindow(MainWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        this.Closing += new CancelEventHandler(Window_Closing);
        InitializeComponent();
    }
    //Обробник натиснення на кнопку додання студента
    private void ButtonAdd_Click(object sender, EventArgs e)
    {
        Student student = new Student();
        student.FirstName = InputTextBox1.Text;
        student.LastName = InputTextBox2.Text;
        student.Sex = InputTextBox3.Text.ToCharArray()[0];
        student.Year = Int32.Parse(InputTextBox4.Text);
        Group group = getParentWindow()
        .getDaoFactory().getGroupDAO().GetById(Group.Id);
        student.Group = group;
        group.StudentList.Add(student);
        getParentWindow().getDaoFactory()

        .getStudentDAO().SaveOrUpdate(student);
        IList<Student> studentList = getParentWindow()
        .getDaoFactory().getStudentDAO()
        .getStudentsByGroup(group.Id);
        getParentWindow().StudentGrid.ItemsSource = studentList;
        closeWindow();
    }
    //Обробник натиснення на кнопку редагування студента
    private void ButtonEdit_Click(object sender, EventArgs e)
    {
        Student student = getParentWindow()
        .getDaoFactory().getStudentDAO().GetById(Student.Id);
        student.FirstName = InputTextBox1.Text;
        student.LastName = InputTextBox2.Text;
        student.Sex = InputTextBox3.Text.ToCharArray()[0];
        student.Year = Int32.Parse(InputTextBox4.Text);
        getParentWindow().getDaoFactory().getStudentDAO()
        .SaveOrUpdate(student);
        getParentWindow().StudentGrid.ItemsSource = getParentWindow()
        .getDaoFactory().getStudentDAO()
        .getStudentsByGroup(Group.Id);
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
        this.InputTextBox4.Text = "";
        this.Hide();
    }
}

