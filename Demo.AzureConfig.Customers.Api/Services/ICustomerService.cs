using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Models;
using Demo.AzureConfig.Customers.Api.Services.Requests;

namespace Demo.AzureConfig.Customers.Api.Services
{
    public interface ICustomerService
    {
        Task<Result<Customer>> SearchCustomerAsync(SearchCustomerByIdRequest request);
        Task<Result> CreateAsync(CreateCustomerRequest request);
    }
}