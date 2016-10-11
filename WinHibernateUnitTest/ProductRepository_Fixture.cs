using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using WinHibernate.Domain;
using NHibernate.Tool.hbm2ddl;
using WinHibernate.Repositories;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace WinHibernateUnitTest
{
    class ProductRepository_Fixture
    {
        private ISessionFactory _sessionFactory;

        private Configuration _configuration;



        [TestFixtureSetUp]

        public void TestFixtureSetUp()

        {

            _configuration = new Configuration();

            _configuration.Configure();

            _configuration.AddAssembly(typeof(Product).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();

        }

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(_configuration).Execute(true, true, false);
            CreateInitialData();
        }

        [Test]
        public void Can_add_new_product()
        {
            var product = new Product { Name = "Apple", Category = "Fruits" };
            IProductRepository repository = new ProductRepository();
            repository.Add(product);

            // use session to try to load the product

            using (ISession session = _sessionFactory.OpenSession())

            {

                var fromDb = session.Get<Product>(product.Id);

                // Test that the product was successfully inserted

                Assert.IsNotNull(fromDb);

                Assert.AreNotSame(product, fromDb);

                Assert.AreEqual(product.Name, fromDb.Name);

                Assert.AreEqual(product.Category, fromDb.Category);

            }
        }

        [Test]
        public void DllRef()
        {
            //1、利用反射进行动态加载和调用.
            Assembly assembly = Assembly.LoadFrom("E:\\Work\\tmp\\DllDemo.dll"); //利用dll的路径加载,同时将此程序集所依赖的程序集加载进来,需后辍名.dll
            //Assembly.LoadFile 只加载指定文件，并不会自动加载依赖程序集.Assmbly.Load无需后辍名
            //2、加载dll后,需要使用dll中某类.
            Type type = assembly.GetType("DllDemo.BasicDll");//用类型的命名空间和名称获得类型
            //3、需要实例化类型,才可以使用,参数可以人为的指定,也可以无参数,静态实例可以省略
            //Object obj = Activator.CreateInstance(type);//利用指定的参数实例话类型
            //4、调用类型中的某个方法:
            //需要首先得到此方法
            //MethodInfo mi = type.GetMethod("CountFirst");//通过方法名称获得方法 
            //5、然后对方法进行调用,多态性利用参数进行控制
            //int rt=(int) mi.Invoke(obj,null);//根据参数直线方法,返回值就是原方法的返回值
            //Assert.AreEqual(rt, 100);

            object[] args = new object[2];
            args[0] = 500;
            args[1] = "600";
            Object obj = Activator.CreateInstance(type);
            System.Type[] tp = new Type[2];
            tp[0] = typeof(System.Int16);
            tp[1] = typeof(System.String);
            MethodInfo mi = type.GetMethod("ToString",tp);
            ParameterInfo [] pi=mi.GetParameters();
            string r=(string)mi.Invoke(obj, args);
            Assert.AreEqual(r, "500600");

        }



        [Test]

        public void Can_update_existing_product()

        {

            var product = _products[0];

            product.Name = "Yellow Pear";

            IProductRepository repository = new ProductRepository();

            repository.Update(product);



            // use session to try to load the product

            using (ISession session = _sessionFactory.OpenSession())

            {

                var fromDb = session.Get<Product>(product.Id);

                Assert.AreEqual(product.Name, fromDb.Name);

            }

        }



        private readonly Product[] _products = new[]

                         {

                     new Product {Name = "Melon", Category = "Fruits"},

                     new Product {Name = "Pear", Category = "Fruits"},

                     new Product {Name = "Milk", Category = "Beverages"},

                     new Product {Name = "Coca Cola", Category = "Beverages"},

                     new Product {Name = "Pepsi Cola", Category = "Beverages"},

                 };



        private void CreateInitialData()

        {



            using (ISession session = _sessionFactory.OpenSession())

            using (ITransaction transaction = session.BeginTransaction())

            {

                foreach (var product in _products)

                    session.Save(product);

                transaction.Commit();

            }

        }

        [Test]
        public void Can_remove_existing_product()
        {
            var product = _products[0];
            IProductRepository repository = new ProductRepository();
            repository.Remove(product);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Product>(product.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void Can_get_existing_product_by_id()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetById(_products[1].Id);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].Name, fromDb.Name);
        }

        [Test]
        public void Can_get_existing_product_by_name()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetByName(_products[1].Name);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].Id, fromDb.Id);
        }

        [Test]
        public void Can_get_existing_products_by_category()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetByCategory("Fruits");

            Assert.AreEqual(2, fromDb.Count);
            Assert.IsTrue(IsInCollection(_products[0], fromDb));
            Assert.IsTrue(IsInCollection(_products[1], fromDb));
        }

        private bool IsInCollection(Product product, ICollection<Product> fromDb)
        {
            foreach (var item in fromDb)
                if (product.Id == item.Id)
                    return true;
            return false;
        }

    }
}
