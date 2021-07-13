using Demo.AzureConfig.Customers.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Demo.AzureConfig.Customers.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    var configuration = builder.Build();
                    var azureAppConfigurationUrl = configuration["AzureAppConfigurationUrl"];
                    builder.RegisterAzureAppConfiguration(azureAppConfigurationUrl);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}