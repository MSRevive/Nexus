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

            if (Config.GetValue<bool>("IPWhitelist:EnableMiddleware"))
            {
                Dictionary<string, bool> ipWhitelist = new Dictionary<string, bool>();

                try
                {
                    var text = File.ReadAllText(Config["IPWhitelist:File"]);
                    ipWhitelist = JsonConvert.DeserializeObject<Dictionary<string, bool>>(text);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("whitelist file not found.");
                    return;
                }

                app.UseMiddleware<Middleware.IpWhitelist>();
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
