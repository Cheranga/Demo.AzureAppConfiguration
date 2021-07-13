using System;
using Azure.Identity;
using Demo.AzureConfig.Customers.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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

                    var azureSharedConfigurationUrl = configuration["AzureSharedConfigurationUrl"];
                    builder.AddAzureAppConfiguration(options =>
                    {
                        var credentials = new DefaultAzureCredential();
                        options.Connect(new Uri(azureSharedConfigurationUrl), credentials)
                            .ConfigureKeyVault(vaultOptions =>
                            {
                                vaultOptions.SetCredential(credentials);
                            })
                            .ConfigureRefresh(refreshOptions =>
                            {
                                refreshOptions.Register("FeatureManagement").SetCacheExpiration(TimeSpan.FromSeconds(10));
                            })
                            .UseFeatureFlags(flagOptions =>
                            {
                                flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(10);
                            });
                    });
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}