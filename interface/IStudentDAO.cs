using System.Collections.Generic;
namespace Lab1;

public interface IStudentDAO : IGenericDAO<Student>
{
    IList<Student> getStudentsByGroup(long groupId);
}