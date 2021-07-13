using System;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Models;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Queries
{
    public class SearchCustomerByIdQueryHandler : IQueryHandler<SearchCustomerByIdQuery, Customer>
    {
        public async Task<Result<Customer>> ExecuteAsync(SearchCustomerByIdQuery query)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            return Result<Customer>.Success(new Customer
            {
                Id = "666"
            });
        }
    }
}