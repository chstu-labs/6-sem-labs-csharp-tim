using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace Lab1
{
    class OrderRepository : IOrderRepository
    {
        private OrderRepository() { }
        private NpgsqlConnection connection;
        private static IOrderRepository instance;

        public static IOrderRepository GetInstance()
        {
            if (null == instance)
            {
                instance = new OrderRepository();
            }
            return instance;
        }

        private NpgsqlConnection GetConnection()
        {
            if (null == connection)
            {
                String connectionStr = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=2809; Database=dotnet;";
                connection = new NpgsqlConnection(connectionStr);
                connection.Open();
            }
            return connection;
        }

        public Order FindById(long id)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = GetConnection();
            command.CommandText = "SELECT * FROM orders WHERE id = @id";
            AddParameterToCommand(command, "@id", DbType.Int64, id);
            DbDataReader row = command.ExecuteReader();
            Order order = null;
            while (row.Read())
            {
                long orderId = (long)row["id"];
                String customerName = (String)row["customername"];
                String pizzaSize = (String)row["pizzasize"];
                String customerSex = (String)row["customersex"];
                int price = (int)row["price"];
                order = new Order();
                order.Id = orderId;
                order.CustomerName = customerName;
                order.PizzaSize = pizzaSize;
                order.CustomerSex = customerSex;
                order.Price = price;
            }
            row.Close();
            return order;
        }

        public IList<Order> GetAll()
        {
            IList<Order> orderList = new List<Order>();
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = GetConnection();
            command.CommandText = "SELECT * FROM orders";
            DbDataReader row = command.ExecuteReader();
            while (row.Read())
            {
                long id = (long)row["id"];
                String customerName = (String)row["customername"];
                String pizzaSize = (String)row["pizzasize"];
                String customerSex = (String)row["customersex"];
                int price = (int)row["price"];
                Order order = new Order();
                order.Id = id;
                order.CustomerName = customerName;
                order.PizzaSize = pizzaSize;
                order.CustomerSex = customerSex;
                order.Price = price;
                orderList.Add(order);
            }
            row.Close();
            return orderList;
        }

        public void Save(Order order)
        {
            if (string.IsNullOrEmpty(order.CustomerName))
            {
                throw new ArgumentException("CustomerName cannot be null or empty.");
            }
            if (order.CustomerName.Length > 50)
            {
                throw new ArgumentException("CustomerName cannot be longer than 50 characters.");
            }

            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = GetConnection();
            command.CommandText = "INSERT INTO orders(customername, pizzasize, customersex, price) VALUES(@customername, @pizzasize, @customersex, @price)";
            AddParameterToCommand(command, "@customername", DbType.String, order.CustomerName);
            AddParameterToCommand(command, "@pizzasize", DbType.String, order.PizzaSize);
            AddParameterToCommand(command, "@customersex", DbType.String, order.CustomerSex);
            AddParameterToCommand(command, "@price", DbType.Int32, order.Price);
            command.ExecuteNonQuery();
        }

        public void Update(Order order)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = GetConnection();
            command.CommandText = "UPDATE orders SET customername = @customername, pizzasize = @pizzasize, customersex = @customersex, price = @price WHERE id = @id";
            AddParameterToCommand(command, "@id", DbType.Int64, order.Id);
            AddParameterToCommand(command, "@customername", DbType.String, order.CustomerName);
            AddParameterToCommand(command, "@pizzasize", DbType.String, order.PizzaSize);
            AddParameterToCommand(command, "@customersex", DbType.String, order.CustomerSex);
            AddParameterToCommand(command, "@price", DbType.Int32, order.Price);
            command.ExecuteNonQuery();
        }

        public void Delete(long id)
        {
            DbCommand command = NpgsqlFactory.Instance.CreateCommand();
            command.Connection = GetConnection();
            command.CommandText = "DELETE FROM orders WHERE id=@id";
            AddParameterToCommand(command, "@id", DbType.Int64, id);
            command.ExecuteNonQuery();
        }

        private void AddParameterToCommand(DbCommand command, string parameterName, DbType parameterType, object parameterValue)
        {
            NpgsqlParameter parameter = new NpgsqlParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = parameterType;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }

        public void Destroy()
        {
            GetConnection().Close();
        }
    }
}
