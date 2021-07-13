using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Demo.AzureConfig.Customers.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void RegisterAzureAppConfiguration(this IConfigurationBuilder builder, string url)
        {
            var credentials = new DefaultAzureCredential();

            builder.AddAzureAppConfiguration(options =>
            {
                options.Connect(new Uri(url), credentials)
                    .UseFeatureFlags(flagOptions =>
                    {
                        flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(10);
                    });
            });
        }
    }
}