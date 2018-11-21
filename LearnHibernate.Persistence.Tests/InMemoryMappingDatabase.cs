using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LearnHibernate.Persistence.Mappings;
using LearnHibernate.Persistence.Mappings.FNH;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
            #region Programmatic Configuration assuming .hbm.xml files are embedded resources
            //var config = new Configuration()
            //    .SetProperty(Environment.ReleaseConnections, "on_close")
            //    .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
            //    .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
            //    .SetProperty(Environment.ConnectionString, "data source=:memory:")
            //    //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Employee.hbm.xml")
            //    //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Benefit.hbm.xml")
            //    //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Address.hbm.xml")
            //    //.AddFile(@"D:\Dev\AdventuresWithCode\LearnHibernate\LearnHibernate.Persistence\Mappings\Xml\Community.hbm.xml");
            //    .AddAssembly("LearnHibernate.Persistence");

            #endregion


            #region Loquacious Configuration

            //var config = new Configuration();
            //config.DataBaseIntegration(db =>
            //{
            //    db.Dialect<SQLiteDialect>();
            //    db.Driver<SQLite20Driver>();
            //    db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
            //    db.ConnectionString = "data source=:memory:";
            //    db.LogFormattedSql = true;
            //    db.LogSqlInConsole = true;
            //})
            //.AddMapping(GetMapping());

            #endregion

            #region Fluent Configuration

            var fnhConfig = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .DefaultSchema("LearnNH")
                    .ShowSql()
                    .FormatSql()
                    //.AdoNetBatchSize(50)
                    .InMemory())
            //.CurrentSessionContext<AsyncLocalSessionContext>()
            //.Cache(cacheBuilder => {
            //    cacheBuilder.UseQueryCache();
            //    cacheBuilder.UseSecondLevelCache();
            //})
            //either of the below is fine. Latter is terse
            //.Mappings(mapper => mapper.FluentMappings.AddFromAssembly(typeof(EmployeeFNHMapping).Assembly))
            .Mappings(mapper => mapper.FluentMappings.AddFromAssemblyOf<EmployeeFNHMapping>());

            var config = fnhConfig.BuildConfiguration();
            #endregion

            sessionFactory = config.BuildSessionFactory();
            Session = sessionFactory.OpenSession();

            new SchemaExport(config)
                .SetOutputFile(@".\ddl.sql")
                //.Create(false, true);
                .Execute(true, true, false, Session.Connection, null);

            //To increment update database
            //new SchemaUpdate(config).Execute(false, true);

            //validate that the schema and mappings are in compliance
            //this is ideal for an integration test?
            //new SchemaValidator(config).Validate();

        }

        private HbmMapping GetMapping()
        {
            var mapper = new ModelMapper();
            //mapper.AddMapping<EmployeeMapping>();
            //mapper.AddMapping<BenefitMapping>();
            //mapper.AddMapping<LeaveMapping>();
            //mapper.AddMapping<SeasonTicketLoanMapping>();
            //mapper.AddMapping<TrainingAllowanceMapping>();
            //mapper.AddMapping<CommunityMapping>();

            //The above explicit mappings can be replaced by
            mapper.AddMappings(typeof(EmployeeMapping).Assembly.GetTypes());
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
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
