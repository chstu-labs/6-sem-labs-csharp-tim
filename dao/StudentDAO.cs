using System.Collections.Generic;
using NHibernate;

namespace Lab1;

public class StudentDAO : GenericDAO<Student>, IStudentDAO
{
    public StudentDAO(ISession session) : base(session) { }
    public IList<Student> getStudentsByGroup(long groupId)
    {
        var list = session.CreateSQLQuery(
        "SELECT Students.* FROM Students JOIN Groups" +
        " ON Students.GroupId = Groups.Id" +
        " WHERE Groups.Id=" + groupId)
        .AddEntity("Student", typeof(Student))
        .List<Student>();
        return list;
    }
}
