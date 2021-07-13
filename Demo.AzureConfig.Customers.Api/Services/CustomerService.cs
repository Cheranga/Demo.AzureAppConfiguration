using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Models;
using Demo.AzureConfig.Customers.Api.Services.Requests;
using MediatR;

namespace Demo.AzureConfig.Customers.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Result<Customer>> SearchCustomerAsync(SearchCustomerByIdRequest request)
        {
            var operation = await _mediator.Send(request);
            return operation;
        }

        public async Task<Result> CreateAsync(CreateCustomerRequest request)
        {
            var operation = await _mediator.Send(request);
            return operation;
        }
    }
}