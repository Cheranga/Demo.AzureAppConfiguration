using Demo.AzureConfig.Customers.Api.Core.Application.Requests;
using FluentValidation;

namespace Demo.AzureConfig.Customers.Api.Core.Application.Validators
{
    public class SearchCustomerByIdRequestValidator : ModelValidatorBase<SearchCustomerByIdRequest>
    {
        public SearchCustomerByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}