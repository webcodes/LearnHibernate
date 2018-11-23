namespace LearnHibernate.Api
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using LearnHibernate.Persistence.Mappings.FNH;
    using Microsoft.Extensions.Configuration;
    using NHibernate;
    using NHibernate.Cfg;

    public class LearnNHSessionFactory
    {
        public LearnNHSessionFactory(IConfiguration appConfig)
        {
            var fluentConfig = Fluently.Configure()
                .Database(PostgreSQLConfiguration.Standard
                .DefaultSchema(appConfig.GetValue<string>("DatabaseSchema"))
                .FormatSql()
                .ShowSql()
                .ConnectionString(appConfig.GetConnectionString("LearnNH")))
                .Mappings(mapper => mapper.FluentMappings.AddFromAssemblyOf<EmployeeFNHMapping>());

            var config = fluentConfig.BuildConfiguration();
            config.SessionFactory().GenerateStatistics();

            NHibernateLogger.SetLoggersFactory(new SerilogNHLoggerFactory());

            this.SessionFactory = config.BuildSessionFactory();

#if DEBUG
            new NHibernate.Tool.hbm2ddl.SchemaExport(config)
                .SetOutputFile(@".\pg_ddl.sql")
                .Create(false, false);
#endif
        }

        public ISessionFactory SessionFactory { get; }

        public ISession OpenSession()
        {
            return this.SessionFactory.OpenSession();
        }
    }
}
