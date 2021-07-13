using System;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}