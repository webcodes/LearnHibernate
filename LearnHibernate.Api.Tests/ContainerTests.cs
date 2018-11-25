namespace LearnHibernate.Api.Tests
{
    using System.Linq;
    using DryIoc;
    using DryIoc.Microsoft.DependencyInjection;
    using LearnHibernate.Api.Middleware;
    using LearnHibernate.Core;
    using LearnHibernate.Proxy.GES;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ContainerTests
    {
        /// <summary>
        /// This will only validate the application services that are registered in DryIoC container
        /// This will not validate netcore framework level service registrations.
        /// </summary>
        [Fact]
        public void ApplicationServiceRegistrations_AreValidInLocalEnv()
        {
            var services = new ServiceCollection();
            var env = new HostingEnvironment
            {
                EnvironmentName = "Development",
            };

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Utility.GetContentRootPath())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();

            services.AddSingleton<IConfiguration>(sp => configuration);
            services.AddSingleton<IHostingEnvironment>(sp => env);
            services.AddFakeGESProxy();

            var container = new Container(r => r.WithConcreteTypeDynamicRegistrations())
            //.WithUnknownServiceResolvers(Rules.AutoFallbackDynamicRegistrations(new List<Assembly> { typeof(Startup).Assembly })
                .WithDependencyInjectionAdapter(services)
                .WithCompositionRoot<CompositionRoot>();

            var errors = container.Validate();

            Assert.NotNull(container.Resolve<IValidator>());
            Assert.NotNull(container.Resolve<FakeSiteMinderContextMiddleware>());
            Assert.Empty(errors);
        }

        [Fact]
        public void ApplicationServiceRegistrations_AreValidInHostedEnv()
        {
            var services = new ServiceCollection();
            var env = new HostingEnvironment();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Utility.GetContentRootPath())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .Build();

            services.AddSingleton<IConfiguration>(sp => configuration);
            services.AddSingleton<IHostingEnvironment>(sp => env);
            services.AddGESProxy(configuration.Get<GESConfiguration>());

            var container = new Container(r => r.WithConcreteTypeDynamicRegistrations())
                .WithDependencyInjectionAdapter(services)
                .WithCompositionRoot<CompositionRoot>();

            var errors = container.Validate();

            //TODO: Additional assertions for env specific dependency registrations
            Assert.NotNull(container.Resolve<IValidator>());
            Assert.NotNull(container.Resolve<SiteMinderContextMiddleware>());
            Assert.Empty(errors);
        }

        /// This is an alternate way to validate all the registrations. But
        /// this will fail since most of the f/w level dependencies will not be registered.

        //[Fact]
        //public void AlternativeWay()
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Utility.GetContentRootPath())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var env = new HostingEnvironment
        //    {
        //        EnvironmentName = "Development",
        //    };
        //    var startup = new Startup(configuration, env);
        //    var services = new ServiceCollection();
        //    services.AddTransient<IConfiguration>(sp => configuration);
        //    services.AddTransient<IHostingEnvironment>(sp => env);
        //    startup.ConfigureServices(services);
        //    Assert.True(startup.ValidateServiceRegistrations());
        //}
    }
}
