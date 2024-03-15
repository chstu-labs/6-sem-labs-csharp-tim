using System.Collections.Generic;
namespace Lab1;
//Доменний клас групи

public class Group : EntityBase
{
    private IList<Student> studentList = new List<Student>();
    public virtual string GroupName { get; set; }
    public virtual string CuratorName { get; set; }
    public virtual string HeadmanName { get; set; }
    public virtual IList<Student> StudentList
    {
        get { return studentList; }
        set { studentList = value; }
    }
}
