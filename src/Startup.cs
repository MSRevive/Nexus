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
using Newtonsoft.Json;

namespace MSNexus
{
    public class Startup
    {
        protected IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            Console.WriteLine("API Authenication Enabled.");
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
                        Console.WriteLine("whitelist file not found.");
                    }
                    finally
                    {
                        Console.WriteLine("IpWhitelist middleware.");
                        app.UseMiddleware<Middleware.IpWhitelist>(ipWhitelist);
                    } 
                }

                if (Config.GetValue<bool>("APIAuth:UseKey"))
                {
                    Console.WriteLine("ApiKey middleware.");
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
