using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using WinHibernate.Domain;

namespace WinHibernateUnitTest
{
    [TestFixture]
    class GenerateSchema_Fixture
    {
        [Test]

        public void Can_generate_schema()

        {

            var cfg = new Configuration();

            cfg.Configure();

            cfg.AddAssembly(typeof(Item).Assembly);
            //cfg.AddAssembly(typeof(Product).Assembly);
            new SchemaExport(cfg).Execute(true, true, false);

        }
    }
}
