using System.Collections.Generic;

namespace Lab1
{
    interface IOrderRepository
    {
        public void Save(Order order);
        public void Update(Order order);
        public IList<Order> GetAll();
        public Order FindById(long id);
        public void Delete(long id);
        public void Destroy();
    }
}

