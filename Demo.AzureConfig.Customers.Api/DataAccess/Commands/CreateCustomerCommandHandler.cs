using System;
using System.Net;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Configs;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.DataAccess.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        public const string Active = nameof(Active);
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly StorageTableConfiguration _tableConfiguration;
        private readonly ITableStorageFactory _tableStorageFactory;

        public CreateCustomerCommandHandler(ITableStorageFactory tableStorageFactory, StorageTableConfiguration tableConfiguration, ILogger<CreateCustomerCommandHandler> logger)
        {
            _tableStorageFactory = tableStorageFactory;
            _tableConfiguration = tableConfiguration;
            _logger = logger;
        }

        public async Task<Result> ExecuteAsync(CreateCustomerCommand command)
        {
            try
            {
                var customerId = Guid.NewGuid().ToString("N").ToUpper();
                var customerDataModel = new CustomerDataModel
                {
                    PartitionKey = Active.ToUpper(),
                    RowKey = customerId,
                    Id = customerId,
                    Name = command.Name,
                    Address = command.Address,
                    DateOfBirth = command.DateOfBirth
                };

                var customersTable = await _tableStorageFactory.GetTableAsync(_tableConfiguration.CustomersTable);
                var tableOperation = TableOperation.Insert(customerDataModel);
                var tableOperationResult = await customersTable.ExecuteAsync(tableOperation);

                if (tableOperationResult.HttpStatusCode == (int) HttpStatusCode.NoContent)
                {
                    return Result.Success();
                }

                return Result.Failure(ErrorCodes.CreateCustomerDataError, ErrorMessages.CreateCustomerDataError);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.CreateCustomerDataError);
            }

            return Result.Failure(ErrorCodes.CreateCustomerDataError, ErrorMessages.CreateCustomerDataError);
        }
    }
}