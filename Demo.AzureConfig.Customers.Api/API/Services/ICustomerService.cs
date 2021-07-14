using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Core.Application.Requests;
using Demo.AzureConfig.Customers.Api.Core.Domain.Models;

namespace Demo.AzureConfig.Customers.Api.API.Services
{
    public interface ICustomerService
    {
        Task<Result<Customer>> SearchCustomerAsync(SearchCustomerByIdRequest request);
        Task<Result> CreateAsync(CreateCustomerRequest request);
    }
}