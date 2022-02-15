using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace StatlerWaldorfCorp.LocationService
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[] { };

        public Startup(IWebHostEnvironment env, ILoggerFactory _loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            services.AddMvc();
            services.AddLogging(logging =>
             {
                 logging.AddConsole();
                 logging.AddDebug();
             });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
