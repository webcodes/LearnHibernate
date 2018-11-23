namespace LearnHibernate.Api
{
    using System;
    using DryIoc;
    using LearnHibernate.Api.Decorators;
    using LearnHibernate.Core;
    using LearnHibernate.Core.Commands.Employee;

    internal class CompositionRoot
    {
        public CompositionRoot(IContainer container)
        {
            RegisterSerilog(container);

            RegisterNHibernate(container);

            //TODO: Register SSO auth 

            RegisterCommandHandlers(container);
            RegisterQueryHandlers(container);
            var errors = container.Validate();
        }

        private static void RegisterQueryHandlers(IContainer container)
        {
            //container.RegisterMany(
            //    new[] { typeof(RecentProfilesQueryHandler).Assembly },
            //    serviceTypeCondition: type => type.IsInterface);

            //container.Register(typeof(IQueryHandler<,>), typeof(LoggingQueryHandler<,>), setup: Setup.Decorator);
        }

        private static void RegisterCommandHandlers(IContainer container)
        {
            //Register all the concrete command handlers for the open generic type of ICommandHandler
            //Assumes all the command handlers are in a single assembly
            //register a decorator for providing a nH transaction scope for all command handlers
            // register a decorator for logging all command handler invocations

            container.Register<IValidator, DataAnnotationsValidator>(Reuse.Singleton);

            container.RegisterMany(
                new[] { typeof(AddEmployeeCommandHandler).Assembly },
                serviceTypeCondition: type => type.IsInterface && type == typeof(ICommandHandler<>));
            container.Register(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>), setup: Setup.DecoratorWith(order: 4));
            container.Register(typeof(ICommandHandler<>), typeof(AuthorizationCommandHandlerDecorator<>), setup: Setup.DecoratorWith(order: 3));
            container.Register(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>), setup: Setup.DecoratorWith(order: 2));
            container.Register(typeof(ICommandHandler<>), typeof(TransactionScopeCommandHandlerDecorator<>), setup: Setup.DecoratorWith(order: 1));
        }

        private static void RegisterNHibernate(IContainer container)
        {
            container.Register<LearnNHSessionFactory>(Reuse.Singleton);

            container.Register(
                made: Made.Of(
                    r => ServiceInfo.Of<LearnNHSessionFactory>(),
                    f => f.OpenSession()),
                reuse: Reuse.Scoped);

            #region Alternative
            //var appConfig = container.Resolve<IConfiguration>();

            //var fluentConfig = Fluently.Configure()
            //    .Database(PostgreSQLConfiguration.Standard
            //    .DefaultSchema("LearnNH")
            //    .FormatSql()
            //    .ShowSql()
            //    .ConnectionString(appConfig.GetConnectionString("learnNH")))
            //    .Mappings(mapper => mapper.FluentMappings.AddFromAssemblyOf<EmployeeFNHMapping>());

            //var config = fluentConfig.BuildConfiguration();
            //config.SessionFactory().GenerateStatistics();

            ////TODO: Register nH logging.

            //NHibernateLogger.SetLoggersFactory(new SerilogLoggerFactory());


            //a singleton of the session factory for the schema of LearnNH
            //container.Register(
            //    made: Made.Of(() => config.BuildSessionFactory()),
            //    reuse: Reuse.Singleton);

            //container.Register(
            //    made: Made.Of(r => ServiceInfo.Of<ISessionFactory>(), f => f.OpenSession()),
            //    reuse: Reuse.Scoped);

            #endregion
        }

        private static void RegisterSerilog(IContainer container)
        {
            // default logger
            container.Register(
                Made.Of(() => Serilog.Log.Logger),
                setup: Setup.With(condition: r => r.Parent.ImplementationType == null));

            // contextual logger
            container.Register(
                Made.Of(() => Serilog.Log.ForContext(Arg.Index<Type>(0)), r => r.Parent.ImplementationType),
                setup: Setup.With(condition: r => r.Parent.ImplementationType != null));
        }
    }
}