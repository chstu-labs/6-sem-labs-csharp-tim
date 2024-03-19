using Lab1;

using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace orm.test.Dao
{
    [TestClass]
    public class TestOrderDAO : TestGenericDAO<Order>
    {
        protected IOrderDAO orderDAO = null;
        public TestOrderDAO() : base()
        {
            IOrderDAO orderDAO =
            NHibernateDAOFactory.getInstance().getOrderDAO();
            setDAO(orderDAO);
        }
        protected override void createEntities()
        {
            entity1 = new Order();
            entity1.CustomerName = "Elon";
            entity1.PizzaSize = "Mask";
            entity1.CustomerSex = "M";
            entity1.Price = 1971;
            entity2 = new Order();
            entity2.CustomerName = "Jeff";
            entity2.PizzaSize = "Bezos";

            entity2.CustomerSex = "M";
            entity2.Price = 1964;
            entity3 = new Order();
            entity3.CustomerName = "Bill";
            entity3.PizzaSize = "Gates";
            entity3.CustomerSex = "M";
            entity3.Price = 1955;
            entity4 = new Order();
            entity4.CustomerName = "Mark";
            entity4.PizzaSize = "Zuckerberg";
            entity4.CustomerSex = "M";
            entity4.Price = 1984;
        }
        protected override void checkAllPropertiesDiffer(
        Order entityToCheck1, Order entityToCheck2)
        {
            Assert.AreNotEqual(entityToCheck1.CustomerName,
            entityToCheck2.CustomerName, "Values must be different");
            Assert.AreNotEqual(entityToCheck1.PizzaSize,
            entityToCheck2.PizzaSize, "Values must be different");
            Assert.AreNotEqual(entityToCheck1.Price,
            entityToCheck2.Price, "Values must be different");
        }
        protected override void checkAllPropertiesEqual(
        Order entityToCheck1, Order entityToCheck2)
        {
            Assert.AreEqual(entityToCheck1.CustomerName,
            entityToCheck2.CustomerName, "Values must be equal");
            Assert.AreEqual(entityToCheck1.PizzaSize,
            entityToCheck2.PizzaSize, "Values must be equal");
            Assert.AreEqual(entityToCheck1.CustomerSex,
            entityToCheck2.CustomerSex, "Values must be equal");
            Assert.AreEqual(entityToCheck1.Price,
            entityToCheck2.Price, "Values must be equal");
        }
        [TestMethod]
        public void TestMergeOrder()
        {
            base.TestMergeGeneric();
        }
        [TestMethod]
        public void TestGetByIdOrder()
        {
            base.TestGetByIdGeneric();
        }
        [TestMethod]
        public void TestGetAllOrder()
        {
            base.TestGetAllGeneric();
        }
        [TestMethod]
        public void TestDeleteOrder()
        {
            base.TestDeleteGeneric();
        }
        [TestMethod]
        public void TestGetTop10OrdersByPrice()
        {
            IOrderDAO orderDAO = NHibernateDAOFactory.getInstance().getOrderDAO();
            var expectedMaxCount = 10;
            var result = orderDAO.GetTop10OrdersByPrice();
            Assert.IsTrue(result.Count <= expectedMaxCount, "Count must be less than or equal to 10");
            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.IsTrue(result[i].Price >= result[i + 1].Price, "Orders must be sorted by price in descending order");
            }
        }
    }

}