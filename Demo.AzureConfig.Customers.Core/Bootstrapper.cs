using Microsoft.Extensions.DependencyInjection;

namespace Demo.AzureConfig.Customers.Core
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            return services;
        }
    }
}