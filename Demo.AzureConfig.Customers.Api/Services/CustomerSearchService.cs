using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.Models;
using Demo.AzureConfig.Customers.Api.Services.Requests;
using MediatR;

namespace Demo.AzureConfig.Customers.Api.Services
{
    public class CustomerSearchService : ICustomerSearchService
    {
        private readonly IMediator _mediator;

        public CustomerSearchService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<Result<Customer>> SearchAsync(SearchCustomerByIdRequest request)
        {
            var operation = await _mediator.Send(request);
            return operation;
        }
    }
}