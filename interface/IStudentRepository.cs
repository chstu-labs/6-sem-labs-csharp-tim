using System.Collections.Generic;

namespace Lab1
{
    interface IStudentRepository
    {
        public void Save(Student student);
        public void Update(Student student);
        public IList<Student> GetAll();
        public Student FindById(long id);
        public void Delete(long id);
        public void Destroy();
    }
}
