using System;

namespace Demo.AzureConfig.Customers.Api.Core.Application.Messaging
{
    public class CustomerCreatedEvent
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}