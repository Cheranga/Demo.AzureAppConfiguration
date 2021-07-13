using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Models;
using Demo.AzureConfig.Customers.Api.Services.Requests;

namespace Demo.AzureConfig.Customers.Api.Services
{
    public interface ICustomerSearchService
    {
        Task<Result<Customer>> SearchAsync(SearchCustomerByIdRequest request);
    }
}