namespace LearnHibernate.Api
{
    using System;
    using System.Linq;
    using DryIoc;
    using DryIoc.Microsoft.DependencyInjection;
    using LearnHibernate.Api.Middleware;
    using LearnHibernate.Core;
    using LearnHibernate.Proxy.GES;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private IContainer container;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddControllersAsServices();

            services.AddMemoryCache(options =>
            {
                options.SizeLimit = 100_000; // this is unit agnostic count and not size in B/KB/MB. The setter needs to set the size.
            });

            services.AddAuthorization(AuthorisationPolicy.GetPolicies);
            if (!this.HostingEnvironment.IsDevelopment())
            {
                var gesConfig = this.Configuration.Get<GESConfiguration>();
                services.AddSingleton<GESConfiguration>(sp => gesConfig);
                services.AddGESProxy(gesConfig);
            }
            else
            {
                services.AddFakeGESProxy();
            }

            this.container = new Container(r => r.WithConcreteTypeDynamicRegistrations())
                .WithDependencyInjectionAdapter(services)
                .WithCompositionRoot<CompositionRoot>();
            return this.container.ConfigureServiceProvider<CompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<FakeSiteMinderContextMiddleware>();
            }
            else
            {
                app.UseHsts();
                app.UseMiddleware<SiteMinderContextMiddleware>();
            }

            app.UseMiddleware<GESClaimsMiddleware>();
            app.UseMvc();
        }

        // public bool ValidateServiceRegistrations()
        // {
        //    var errors = this.container.Validate();
        //    return errors.Any();
        // }
    }
}
