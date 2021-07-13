using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Configs;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.DataAccess.Models;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Queries
{
    public class SearchCustomerByIdQueryHandler : IQueryHandler<SearchCustomerByIdQuery, CustomerDataModel>
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

        public async Task<Result<CustomerDataModel>> ExecuteAsync(SearchCustomerByIdQuery query)
        {
            try
            {
                var customersTable = await _tableStorageFactory.GetTableAsync(_tableConfiguration.CustomersTable);

                var partitionQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "active".ToUpper());
                var rowIdQuery = TableQuery.GenerateFilterCondition("Id", QueryComparisons.Equal, query.CustomerId);
                var combinedQuery = TableQuery.CombineFilters(partitionQuery, TableOperators.And, rowIdQuery);

                var getRecordQuery = new TableQuery<CustomerDataModel>().Where(combinedQuery);
                var queryOperation = await customersTable.ExecuteQuerySegmentedAsync(getRecordQuery, new TableContinuationToken());

                var customer = queryOperation?.Results?.FirstOrDefault();

                return Result<CustomerDataModel>.Success(customer);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, ErrorMessages.SearchByCustomerIdDataError);
            }

            return Result<CustomerDataModel>.Failure(ErrorCodes.SearchByCustomerIdDataError, ErrorMessages.SearchByCustomerIdDataError);
        }
    }
}