using Lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace orm.test.Dao
{
    [TestClass()]
    public abstract class TestGenericDAO<T> where T : EntityBase
    {
        protected TestContext testContextInstance;
        /** DAO that will be tested */
        protected IGenericDAO<T> dao = null;
        /** First entity that will be used in tests */
        protected T entity1 = null;
        /** Second entity that will be used in tests */
        protected T entity2 = null;
        /** Third entity that will be used in tests */
        protected T entity3 = null;
        protected T entity4 = null;

        public TestGenericDAO()
        {
        }
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        /*Getting dao this test case works with*/
        public IGenericDAO<T> getDAO()
        {
            return dao;
        }
        /*Setting dao this test case will work with*/
        public void setDAO(IGenericDAO<T> dao)
        {
            this.dao = dao;
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            NHibernateDAOFactory.getInstance().destroy();
        }
        [TestInitialize]
        public void TestInitialize()
        {
            Assert.IsNotNull(dao,
            "Please, provide IGenericDAO implementation in constructor");
            createEntities();
            Assert.IsNotNull(entity1, "Please, create object for entity1");
            Assert.IsNotNull(entity2, "Please, create object for entity2");
            Assert.IsNotNull(entity3, "Please, create object for entity3");
            checkAllPropertiesDiffer(entity1, entity2);
            checkAllPropertiesDiffer(entity1, entity3);
            checkAllPropertiesDiffer(entity2, entity3);
            saveEntitiesGeneric();
        }
        [TestCleanup]
        public void TestCleanup()
        {
            try
            {
                if ((entity1 = dao.GetById(entity1.Id)) != null)
                {
                    dao.Delete(entity1);
                }
            }
            catch (Exception)
            {
                Assert.Fail("Problem in cleanup method");
            }

            try
            {
                if ((entity2 = dao.GetById(entity2.Id)) != null)
                    dao.Delete(entity2);
            }
            catch (Exception)
            {
                Assert.Fail("Problem in cleanup method");
            }
            try
            {
                if ((entity3 = dao.GetById(entity3.Id)) != null)
                {
                    dao.Delete(entity3);
                }
            }
            catch (Exception)
            {
                Assert.Fail("Problem in cleanup method");
            }
            try
            {
                if ((entity4 = dao.GetById(entity4.Id)) != null)
                {
                    dao.Delete(entity4);
                }
            }
            catch (Exception)
            {
                Assert.Fail("Problem in cleanup method");
            }
            entity1 = null;
            entity2 = null;
            entity3 = null;
            entity4 = null;
        }
        public void TestMergeGeneric()
        {
            dao.Merge(entity4);
            T savedObject = getPersistentObject(entity3);
            Assert.IsNotNull(savedObject,
            "Dao method Merge should return entity if successfull");
            checkAllPropertiesEqual(savedObject, entity3);
        }
        public void TestGetByIdGeneric()
        {
            T foundObject = null;
            // Should not find with inexistant id
            try
            {
                long id = DateTime.Now.ToFileTime();
                foundObject = dao.GetById(id);
                Assert.IsNull(foundObject,
                 "Should return null if id is inexistent");
            }
            catch (Exception)
            {
                Assert.Fail("Should return null if object not found");
            }
            // Getting all three entities

            getEntityGeneric(entity1.Id, entity1);
            getEntityGeneric(entity2.Id, entity2);
            getEntityGeneric(entity3.Id, entity3);
        }
        public void TestGetAllGeneric()
        {
            List<T> list = getListOfAllEntities();
            Assert.IsTrue(list.Contains(entity1),
            "After dao method GetAll list should contain entity1");
            Assert.IsTrue(list.Contains(entity2),
            "After dao method GetAll list should contain entity2");
            Assert.IsTrue(list.Contains(entity3),
            "After dao method GetAll list should contain entity3");
        }
        public void TestDeleteGeneric()
        {
            try
            {
                dao.Delete((T)null);
                Assert.Fail("Should not delete entity will null id");
            }
            catch (Exception)
            {
            }
            // Deleting second entity
            try
            {
                dao.Delete(entity2);
            }
            catch (Exception)
            {
                Assert.Fail("Deletion should be successful of entity2");
            }
            // Checking if other two entities can be still found
            getEntityGeneric(entity1.Id, entity1);
            getEntityGeneric(entity3.Id, entity3);
            // Checking if entity2 can not be found
            try
            {
                T foundEntity = null;
                foundEntity = dao.GetById(entity2.Id);
                Assert.IsNull(foundEntity,
                "After deletion entity should not be found with id "
                + entity2.Id);
            }
            catch (Exception)
            {
                Assert.Fail("Should return null if entity was deleted");
            }
            List<T> list = getListOfAllEntities();
            Assert.IsTrue(list.Contains(entity1),
            "After dao method GetAll list should contain entity1");
            Assert.IsTrue(list.Contains(entity3),
            "After dao method GetAll list should contain entity3");
        }
        protected abstract void createEntities();
        protected abstract void checkAllPropertiesDiffer(

        T entityToCheck1, T entityToCheck2);
        protected abstract void checkAllPropertiesEqual(
        T entityToCheck1, T entityToCheck2);
        protected void saveEntitiesGeneric()
        {
            T savedObject = null;
            try
            {
                dao.Merge(entity1);
                savedObject = getPersistentObject(entity1);
                Assert.IsNotNull(savedObject,
                 "Dao method Merge should return entity if successfull");
                checkAllPropertiesEqual(savedObject, entity1);
                entity1 = savedObject;
            }
            catch (Exception)
            {
                Assert.Fail("Fail to save entity1");
            }
            try
            {
                dao.Merge(entity2);
                savedObject = getPersistentObject(entity2);
                Assert.IsNotNull(savedObject,
                "Dao method Merge should return entity if successfull");
                checkAllPropertiesEqual(savedObject, entity2);
                entity2 = savedObject;
            }
            catch (Exception)
            {
                Assert.Fail("Fail to save entity2");
            }
            try
            {
                dao.Merge(entity3);
                savedObject = getPersistentObject(entity3);
                Assert.IsNotNull(savedObject,
                 "Dao method Merge should return entity if successfull");
                checkAllPropertiesEqual(savedObject, entity3);
                entity3 = savedObject;
            }
            catch (Exception)
            {
                Assert.Fail("Fail to save entity3");
            }
        }
        protected T getPersistentObject(T nonPersistentObject)
        {
            return dao.GetPersistentObject(nonPersistentObject);
        }
        protected void getEntityGeneric(long id, T entity)
        {
            T foundEntity = null;
            try
            {
                foundEntity = dao.GetById(id);
                Assert.IsNotNull(foundEntity,
                "Service method should return entity if successfull");
                checkAllPropertiesEqual(foundEntity, entity);

            }
            catch (Exception)
            {
                Assert.Fail("Failed to get entity with id " + id);
            }
        }
        protected List<T> getListOfAllEntities()
        {
            List<T> list = null;
            // Should get not null and not empty list
            try
            {
                list = dao.GetAll();
            }
            catch (Exception)
            {
                Assert.Fail(
                "Should be able to get entities were added before");
            }
            Assert.IsNotNull(list,
            "Dao method should return list of entities if successfull");
            Assert.IsFalse(list.Count == 0,
            "Dao method should return not empty list if successfull");
            return list;
        }
    }
}