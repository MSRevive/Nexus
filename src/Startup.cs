using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Newtonsoft.Json;

namespace MSNexus
{
    public class Startup
    {
        protected IConfiguration Config { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Config = configuration;
            using(var client = new DAL.Character(configuration))
            {
                client.Database.EnsureCreated();
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite();
            services.AddDbContext<DAL.Character>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Middleware stuff
            if (Config.GetValue<bool>("APIAuth:Enabled"))
            {
                Dictionary<string, bool> ipWhitelist = new Dictionary<string, bool>();

                if (Config.GetValue<bool>("APIAuth:UseIP"))
                {
                    try
                    {
                        var text = File.ReadAllText(Config["APIAuth:IPList"]);
                        ipWhitelist = JsonConvert.DeserializeObject<Dictionary<string, bool>>(text);
                    }
                    catch (FileNotFoundException)
                    {
                        //don't use middleware if file isn't found.
                    }
                    finally
                    {
                        app.UseMiddleware<Middleware.IpWhitelist>(ipWhitelist);
                    } 
                }

                if (Config.GetValue<bool>("APIAuth:UseKey"))
                {
                    app.UseMiddleware<Middleware.ApiKey>(Config["APIAuth:Key"]);
                }
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
