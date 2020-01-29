using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop_WebApp.Models;
using static OnlineShop_WebApp.Repos.DbContext;

namespace OnlineShop_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server = DESKTOP-MT86OA9\\SQLEXPRESS ; Initial Catalog = IShop; Integrated Security=True";
            services.AddTransient<IprContext, PrContext>(provider => new PrContext(connectionString));
            services.AddMvc();
            services.AddSession();
        }
            
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();

            app.UseDeveloperExceptionPage();
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
