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
            throw new NotImplementedException();
        }
    }
}