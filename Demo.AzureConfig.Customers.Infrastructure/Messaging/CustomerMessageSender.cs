using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Demo.AzureConfig.Customers.Core.Application;
using Demo.AzureConfig.Customers.Core.Application.Messaging;
using Demo.AzureConfig.Customers.Core.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;

namespace Demo.AzureConfig.Customers.Infrastructure.Messaging
{
    public class CustomerMessageSender : IMessageSender
    {
        private readonly IFeatureManager _featureManager;
        private readonly ILogger<CustomerMessageSender> _logger;
        private readonly ServiceBusSender _messageSender;

        public CustomerMessageSender(IFeatureManager featureManager, ServiceBusSender messageSender, ILogger<CustomerMessageSender> logger)
        {
            _featureManager = featureManager;
            _messageSender = messageSender;
            _logger = logger;
        }

        public async Task<Result> SendAsync<TMessage>(TMessage message) where TMessage : class
        {
            try
            {
                var isAllowed = await _featureManager.IsEnabledAsync(ApplicationFeatures.PublishMessages.ToString());
                if (!isAllowed)
                {
                    _logger.LogWarning("message publishing feature is disabled");
                    return Result.Success();
                }

                var messageData = JsonConvert.SerializeObject(message);
                var sbMessage = new ServiceBusMessage(messageData);
                await _messageSender.SendMessageAsync(sbMessage);

                return Result.Success();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.MessagePublishError);
            }

            return Result.Failure(ErrorCodes.MessagePublishError, ErrorMessages.MessagePublishError);
        }
    }
}