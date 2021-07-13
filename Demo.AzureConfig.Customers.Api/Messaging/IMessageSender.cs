using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Demo.AzureConfig.Customers.Api.Configs;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;

namespace Demo.AzureConfig.Customers.Api.Messaging
{
    public interface IMessageSender
    {
        Task<Result> SendAsync<TMessage>(TMessage message) where TMessage : class;
    }

    public class CustomerMessageSender : IMessageSender
    {
        private readonly IFeatureManager _featureManager;
        private readonly ServiceBusConfiguration _serviceBusConfig;
        private readonly ILogger<CustomerMessageSender> _logger;

        public CustomerMessageSender(IFeatureManager featureManager, ServiceBusConfiguration serviceBusConfig, ILogger<CustomerMessageSender> logger)
        {
            _featureManager = featureManager;
            _serviceBusConfig = serviceBusConfig;
            _logger = logger;
        }
        
        public async Task<Result> SendAsync<TMessage>(TMessage message) where TMessage : class
        {
            try
            {
                var isAllowed = await _featureManager.IsEnabledAsync(ApplicationFeatures.PublishMessages);
                if (!isAllowed)
                {
                    _logger.LogWarning("message publishing feature is disabled");
                    return Result.Success();
                }
                
                var client = new ServiceBusClient(_serviceBusConfig.SendOnlyConnectionString);
                var sender = client.CreateSender(_serviceBusConfig.Topic);

                var messageData = JsonConvert.SerializeObject(message);
                var sbMessage = new ServiceBusMessage(messageData);
                await sender.SendMessageAsync(sbMessage);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.MessagePublishError);
            }

            return Result.Failure(ErrorCodes.MessagePublishError, ErrorMessages.MessagePublishError);
        }
    }
}