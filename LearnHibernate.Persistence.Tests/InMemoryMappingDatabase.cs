using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Environment = NHibernate.Cfg.Environment;

namespace LearnHibernate.Persistence.Tests
{
    public class InMemoryMappingDatabase : IDisposable
    {
        private readonly ISessionFactory sessionFactory;

        public ISession Session { get; }

        public InMemoryMappingDatabase()
        {
            var config = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=:memory:")
                //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Employee.hbm.xml")
                //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Benefit.hbm.xml")
                //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Address.hbm.xml")
                //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Community.hbm.xml");
                .AddAssembly("LearnHibernate.Persistence");

            sessionFactory = config.BuildSessionFactory();
            Session = sessionFactory.OpenSession();

            var ddlBuilder = new StringBuilder();
            new SchemaExport(config).Execute(true, true, false, Session.Connection, new StringWriter(ddlBuilder));
            Debug.WriteLine(ddlBuilder.ToString());
        }

        public void Dispose()
        {
            if (Session != null)
            {
                Session.Dispose();
            }
            if (sessionFactory != null)
            {
                sessionFactory.Dispose();
            }
        }
    }
}
