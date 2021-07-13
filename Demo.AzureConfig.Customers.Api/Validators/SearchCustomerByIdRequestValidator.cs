using Demo.AzureConfig.Customers.Api.Services.Requests;
using FluentValidation;

namespace Demo.AzureConfig.Customers.Api.Validators
{
    public class SearchCustomerByIdRequestValidator : ModelValidatorBase<SearchCustomerByIdRequest>
    {
        public SearchCustomerByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}