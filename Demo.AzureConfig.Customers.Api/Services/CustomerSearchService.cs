using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.DTO.Requests;
using Demo.AzureConfig.Customers.Api.Models;

namespace Demo.AzureConfig.Customers.Api.Services
{
    public class CustomerSearchService : ICustomerSearchService
    {
        public Task<Result<Customer>> SearchAsync(SearchCustomerByIdRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}