using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Application.Requests;
using Demo.AzureConfig.Customers.Core.Domain;
using Demo.AzureConfig.Customers.Core.Domain.Models;

namespace Demo.AzureConfig.Customers.Api.API.Services
{
    public interface ICustomerService
    {
        Task<Result<Customer>> SearchCustomerAsync(SearchCustomerByIdRequest request);
        Task<Result> CreateAsync(CreateCustomerRequest request);
    }
}