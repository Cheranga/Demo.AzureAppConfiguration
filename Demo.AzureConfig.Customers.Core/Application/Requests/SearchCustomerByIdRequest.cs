using System.Threading;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Application.Queries;
using Demo.AzureConfig.Customers.Core.Domain;
using Demo.AzureConfig.Customers.Core.Domain.Models;
using FluentValidation;
using MediatR;

namespace Demo.AzureConfig.Customers.Core.Application.Requests
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
                return Result<Customer>.Failure(ErrorCodes.SearchCustomerError,ErrorMessages.SearchCustomerError);
            }

            var customerDataModel = operation.Data;
            if (customerDataModel == null)
            {
                return Result<Customer>.Failure(ErrorCodes.CustomerNotFound, ErrorMessages.CustomerNotFound);
            }

            var customer = new Customer
            {
                Id = customerDataModel.Id,
                Name = customerDataModel.Name,
                Address = customerDataModel.Address,
                DateOfBirth = customerDataModel.DateOfBirth
            };
            
            return Result<Customer>.Success(customer);
        }
    }
}