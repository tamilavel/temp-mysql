using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LoyaltyWeb.Models;



namespace LoyaltyWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(300);
            });
            services.AddSession();

            services.AddMvc();

            string _connectionString = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
            _connectionString.Replace("//", "");

            char[] delimiterChars = { '/', ':', '@', '?' };
            string[] strConn = _connectionString.Split(delimiterChars);
            strConn = strConn.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            Config.User = strConn[1];
            Config.Pass = strConn[2];
            Config.Server = strConn[3];
            Config.Database = strConn[4];
            Config.Port = "3306";
            Config.ConnectionString = "host=" + Config.Server + ";port=" + Config.Port + ";database=" + Config.Database + ";uid=" + Config.User + ";pwd=" + Config.Pass + ";";
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
         

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
                    template: "{controller=User}/{action=Login}/{id?}");
            });

        }
    }
}
