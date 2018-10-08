using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyFirstCarSite.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MyFirstCarSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:MyFirstCarSiteVehicles:ConnectionString"]));
            services.AddTransient<IVehicleRepository, EFVehicleRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{make}/Page{vehiclePage:int}",
                    defaults: new { controller = "Vehicle", action = "List" }
                    );

                routes.MapRoute(
                    name: null,
                    template: "Page{vehiclePage:int}",
                    defaults: new { controller = "Vehicle",
                    action = "List", vehiclePage = 1}
                    );

                routes.MapRoute(
                    name: null,
                    template: "{make}",
                    defaults: new
                    {
                        controller = "Vehicle",
                        action = "List",
                        vehiclePage = 1
                    }
                    );

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Vehicle",
                        action = "List",
                        vehiclePage = 1
                    }
                    );


                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");
            });
            SeedData.EnsurePopulated(app);
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
