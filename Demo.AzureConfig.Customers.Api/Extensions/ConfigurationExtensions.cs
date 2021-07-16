using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace Demo.AzureConfig.Customers.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void RegisterAzureAppConfiguration(this IConfigurationBuilder builder, HostBuilderContext context, IConfiguration configuration)
        {
            var credentials = new DefaultAzureCredential();
            // Application specific configurations
            builder.AddAzureAppConfiguration(options =>
            {
                var url = configuration["AzureAppConfigurationUrl"];
                options.Connect(new Uri(url), credentials)
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
            // Common configurations
            builder.AddAzureAppConfiguration(options =>
            {
                var azureSharedConfigurationUrl = configuration["AzureSharedConfigurationUrl"];
                
                options.Connect(new Uri(azureSharedConfigurationUrl), credentials)
                    .Select(KeyFilter.Any)
                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                    .ConfigureKeyVault(vaultOptions =>
                    {
                        vaultOptions.SetCredential(credentials);
                    })
                    .ConfigureRefresh(refreshOptions =>
                    {
                        // refreshOptions.Register(".appconfig.featureflag", context.HostingEnvironment.EnvironmentName, true).SetCacheExpiration(TimeSpan.FromSeconds(10));
                        refreshOptions.Register("FeatureManagement", context.HostingEnvironment.EnvironmentName, true).SetCacheExpiration(TimeSpan.FromSeconds(10));
                    })
                    .UseFeatureFlags(flagOptions =>
                    {
                        flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(10);
                        flagOptions.Label = context.HostingEnvironment.EnvironmentName;
                    });
            });
        }
    }
}