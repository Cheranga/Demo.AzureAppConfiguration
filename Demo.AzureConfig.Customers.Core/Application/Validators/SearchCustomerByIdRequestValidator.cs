using Demo.AzureConfig.Customers.Core.Application.Requests;
using Demo.AzureConfig.Customers.Core.Domain;
using FluentValidation;

namespace Demo.AzureConfig.Customers.Core.Application.Validators
{
    public class SearchCustomerByIdRequestValidator : ModelValidatorBase<SearchCustomerByIdRequest>
    {
        public SearchCustomerByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}