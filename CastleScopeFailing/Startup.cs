using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Castle.Facilities.AspNetCore;
using Castle.Windsor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Component = Castle.MicroKernel.Registration.Component;

namespace CastleScopeFailing
{
    public class Startup
    {
		private readonly IWindsorContainer _container = new WindsorContainer();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
			_container.CrossWiresInto(services);
			_container.Register(Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>());
	        _container.Register(Component.For<IScopeSomething>().ImplementedBy<ScopeSomething>().LifestyleScoped().CrossWired().IsDefault());
			services.AddMvc();
	        return services.AddWindsor(_container);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
             
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
