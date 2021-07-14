using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Core.Application.Requests;
using Demo.AzureConfig.Customers.Api.Core.Domain.Models;
using MediatR;

namespace Demo.AzureConfig.Customers.Api.API.Services
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