namespace LearnHibernate.Api
{
    using System;
    using DryIoc;
    using DryIoc.Microsoft.DependencyInjection;
    using LearnHibernate.Api.Middleware;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddControllersAsServices();

            return new Container()
                .WithDependencyInjectionAdapter(services)
                .ConfigureServiceProvider<CompositionRoot>();
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

            
            app.UseMvc();
        }
    }
}
