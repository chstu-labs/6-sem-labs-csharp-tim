using System.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Npgsql;

namespace Lab1
{
    class StudentRepository : IStudentRepository
    {
        private StudentRepository() { }
        private NpgsqlConnection connection;
        private static IStudentRepository instance;
        public static IStudentRepository getInstance()
        {
            if (null == instance)
            {
                instance = new StudentRepository();
            }
            return instance;
        }

        private NpgsqlConnection getConnection()
        {
            if (null == connection)
            {
                String connectionStr = "Server=127.0.0.1; Port=5432; User Id = postgres; Password = 2809; Database = dotnet;";
                connection = new NpgsqlConnection(connectionStr);
                connection.Open();
            }
            return connection;
        }
        public Student FindById(long id)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = getConnection();
            command.CommandText = "SELECT * FROM Students WHERE id = @id";
            addParameterToCommand(command, "@id", DbType.Int64, id);
            DbDataReader row = command.ExecuteReader();
            Student student = null;
            while (row.Read())
            {
                long studentId = (long)row["id"];
                String firstName = (String)row["firstname"];
                String lastName = (String)row["lastname"];
                String sex = (String)row["sex"];
                int age = (int)row["age"];
                student = new Student();
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Sex = sex;
                student.Age = age;
            }
            row.Close();
            return student;
        }
        public IList<Student> GetAll()
        {
            IList<Student> studentList = new List<Student>();
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = getConnection();
            command.CommandText = "SELECT * FROM Students";
            DbDataReader row = command.ExecuteReader();
            while (row.Read())
            {
                long id = (long)row["id"];
                String firstName = (String)row["firstname"];
                String lastName = (String)row["lastname"];
                String sex = (String)row["sex"];
                int age = (int)row["age"];
                Student student = new Student();
                student.Id = id;
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Sex = sex;
                student.Age = age;
                studentList.Add(student);
            }
            row.Close();
            return studentList;
        }

        public void Save(Student student)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = getConnection();
            command.CommandText = "INSERT INTO Students(firstname, lastname, sex, age) VALUES(@firstname, @lastname, @sex, @age)";
            addParameterToCommand(command, "@firstname", DbType.String,
            student.FirstName);
            addParameterToCommand(command, "@lastname", DbType.String,
            student.LastName);
            addParameterToCommand(command, "@sex", DbType.String,
            student.Sex);
            addParameterToCommand(command, "@age", DbType.Int32,
            student.Age);
            command.ExecuteNonQuery();
        }
        public void Update(Student student)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = getConnection();
            command.CommandText = "UPDATE Students SET firstname = @firstname, lastname = @lastname, sex = @sex, age = @age WHERE id = @id";
            addParameterToCommand(command, "@id", DbType.Int64,
     student.Id);
            addParameterToCommand(command, "@firstname", DbType.String,
            student.FirstName);
            addParameterToCommand(command, "@lastname", DbType.String,
            student.LastName);
            addParameterToCommand(command, "@sex", DbType.String,
            student.Sex);
            addParameterToCommand(command, "@age", DbType.Int32,
            student.Age);
            command.ExecuteNonQuery();
        }
        public void Delete(long id)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = getConnection();
            command.CommandText = "DELETE FROM Students WHERE id=@id";
            addParameterToCommand(command, "@id", DbType.Int64, id);
            command.ExecuteNonQuery();
        }
        private void addParameterToCommand(DbCommand command,
        string parameterName, DbType parameterType,
        object parameterValue)
        {
            NpgsqlParameter parameter = new NpgsqlParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = parameterType;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
        public void Destroy()
        {
            getConnection().Close();
        }
    }
}