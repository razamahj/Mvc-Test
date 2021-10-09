using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcTest.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitialiseDB(host);
                
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitialiseDB(IHost host) 
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                DbInitialiser.Initailise(applicationDbContext);
            }
        }
    }
}
