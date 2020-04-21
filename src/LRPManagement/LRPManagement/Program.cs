using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPManagement.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LRPManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<LrpDbContext>();
                    DbInitialiser.Initialise(context);
                    var authContext = services.GetRequiredService<AccountsContext>();
                    authContext.Database.EnsureCreated();
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "A Seeding Error Occured");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://127.0.0.1:5001");
                });
    }
}
