using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Application;
using Demo.AzureConfig.Customers.Core.Application.Queries;
using Demo.AzureConfig.Customers.Core.Domain;
using Demo.AzureConfig.Customers.Core.Domain.Models;
using Demo.AzureConfig.Customers.Infrastructure.Configs;
using Demo.AzureConfig.Customers.Infrastructure.DataAccess.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace Demo.AzureConfig.Customers.Infrastructure.DataAccess.QueryHandlers
{
    public class SearchCustomerByIdQueryHandler : IQueryHandler<SearchCustomerByIdQuery, Customer>
    {
        private readonly ILogger<SearchCustomerByIdQueryHandler> _logger;
        private readonly ITableStorageFactory _tableStorageFactory;
        private readonly StorageTableConfiguration _tableConfiguration;

        public SearchCustomerByIdQueryHandler(ITableStorageFactory tableStorageFactory, StorageTableConfiguration tableConfiguration, ILogger<SearchCustomerByIdQueryHandler> logger)
        {
            _tableStorageFactory = tableStorageFactory;
            _tableConfiguration = tableConfiguration;
            _logger = logger;
        }

        public async Task<Result<Customer>> ExecuteAsync(SearchCustomerByIdQuery query)
        {
            try
            {
                var customersTable = await _tableStorageFactory.GetTableAsync(_tableConfiguration.CustomersTable);

                var partitionQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "active".ToUpper());
                var rowIdQuery = TableQuery.GenerateFilterCondition("Id", QueryComparisons.Equal, query.CustomerId);
                var combinedQuery = TableQuery.CombineFilters(partitionQuery, TableOperators.And, rowIdQuery);

                var getRecordQuery = new TableQuery<CustomerDataModel>().Where(combinedQuery);
                var queryOperation = await customersTable.ExecuteQuerySegmentedAsync(getRecordQuery, new TableContinuationToken());

                var customerDataModel = queryOperation?.Results?.FirstOrDefault();

                var customer = customerDataModel == null
                    ? null
                    : new Customer
                    {
                        Id = customerDataModel.Id,
                        Name = customerDataModel.Name,
                        Address = customerDataModel.Address,
                        DateOfBirth = customerDataModel.DateOfBirth
                    };

                return Result<Customer>.Success(customer);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.SearchByCustomerIdDataError);
            }

            return Result<Customer>.Failure(ErrorCodes.SearchByCustomerIdDataError, ErrorMessages.SearchByCustomerIdDataError);
        }
    }
}