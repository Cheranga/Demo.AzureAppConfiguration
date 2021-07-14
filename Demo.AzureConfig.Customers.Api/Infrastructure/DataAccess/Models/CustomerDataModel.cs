using System;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.AzureConfig.Customers.Api.Infrastructure.DataAccess.Models
{
    public class CustomerDataModel : TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}