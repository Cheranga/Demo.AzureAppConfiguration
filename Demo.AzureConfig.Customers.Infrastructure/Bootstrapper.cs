using Azure.Messaging.ServiceBus;
using Demo.AzureConfig.Customers.Core.Application.Commands;
using Demo.AzureConfig.Customers.Core.Application.Messaging;
using Demo.AzureConfig.Customers.Core.Application.Queries;
using Demo.AzureConfig.Customers.Core.Domain.Models;
using Demo.AzureConfig.Customers.Infrastructure.Configs;
using Demo.AzureConfig.Customers.Infrastructure.DataAccess;
using Demo.AzureConfig.Customers.Infrastructure.DataAccess.CommandHandlers;
using Demo.AzureConfig.Customers.Infrastructure.DataAccess.Models;
using Demo.AzureConfig.Customers.Infrastructure.DataAccess.QueryHandlers;
using Demo.AzureConfig.Customers.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.AzureConfig.Customers.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterDataAccess(services, configuration);
            RegisterMessaging(services, configuration);


            return services;
        }

        private static void RegisterMessaging(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                var serviceBusConfiguration = configuration.GetSection(nameof(ServiceBusConfiguration)).Get<ServiceBusConfiguration>();
                var client = new ServiceBusClient(serviceBusConfiguration.SendOnlyConnectionString);
                var sender = client.CreateSender(serviceBusConfiguration.Topic);

                return sender;
            });

            services.AddSingleton<IMessageSender, CustomerMessageSender>();
        }

        private static void RegisterDataAccess(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider =>
            {
                var tableStorageConfig = configuration.GetSection(nameof(StorageTableConfiguration)).Get<StorageTableConfiguration>();
                return tableStorageConfig;
            });
            
            services.AddSingleton<ITableStorageFactory, StorageTableFactory>();
            // Query handlers
            services.AddScoped<IQueryHandler<SearchCustomerByIdQuery, Customer>, SearchCustomerByIdQueryHandler>();
            // Command handlers
            services.AddScoped<ICommandHandler<CreateCustomerCommand>, CreateCustomerCommandHandler>();
        }
    }
}