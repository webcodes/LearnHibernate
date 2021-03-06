﻿namespace LearnHibernate.Api.Tests
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
                .WithDependencyInjectionAdapter(services)
                .WithCompositionRoot<CompositionRoot>();

            var errors = container.Validate(svcRegInfo =>
                                            svcRegInfo.ServiceType.AssemblyQualifiedName
                                            .StartsWith("LearnHibernate"));

            Assert.NotNull(container.Resolve<IValidator>());
            Assert.NotNull(container.Resolve<FakeSiteMinderContextMiddleware>());

            // Serilog registration shows an error on Validate call although it works as expected.
            // Seems like a problem with Validate call.
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

            // Expect to resolve app level dependencies correctly?
            // Anything that is in LearnHibernate assemblies?
            var errors = container.Validate(svcRegInfo =>
            svcRegInfo.ServiceType.AssemblyQualifiedName
            .StartsWith("LearnHibernate"));

            // TODO: Additional assertions for env specific dependency registrations
            Assert.NotNull(container.Resolve<IValidator>());
            Assert.NotNull(container.Resolve<SiteMinderContextMiddleware>());
            Assert.Empty(errors);
        }
    }
}
