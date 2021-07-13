using System.Threading;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.DataAccess.Queries;
using Demo.AzureConfig.Customers.Api.Models;
using FluentValidation;
using MediatR;

namespace Demo.AzureConfig.Customers.Api.Services.Requests
{
    public class SearchCustomerByIdRequest : IRequest<Result<Customer>>
    {
        public string Id { get; set; }
    }
    
    public class SearchCustomerByIdRequestHandler : IRequestHandler<SearchCustomerByIdRequest, Result<Customer>>
    {
        private readonly IValidator<SearchCustomerByIdRequest> _validator;
        private readonly IQueryHandler<SearchCustomerByIdQuery, Customer> _queryHandler;

        public SearchCustomerByIdRequestHandler(IValidator<SearchCustomerByIdRequest> validator, IQueryHandler<SearchCustomerByIdQuery, Customer> queryHandler)
        {
            _validator = validator;
            _queryHandler = queryHandler;
        }
        
        public async Task<Result<Customer>> Handle(SearchCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<Customer>.Failure(ErrorCodes.InvalidCustomerSearch, validationResult);
            }

            var query = new SearchCustomerByIdQuery
            {
                CustomerId = request.Id
            };

            var operation = await _queryHandler.ExecuteAsync(query);
            if (!operation.Status)
            {
                return operation;
            }

            var customer = operation.Data;
            if (customer == null)
            {
                return Result<Customer>.Failure(ErrorCodes.CustomerNotFound, ErrorMessages.CustomerNotFound);
            }

            return Result<Customer>.Success(customer);
        }
    }
}