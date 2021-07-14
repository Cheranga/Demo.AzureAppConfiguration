using System;
using Demo.AzureConfig.Customers.Api.Core.Application.Requests;
using FluentValidation;

namespace Demo.AzureConfig.Customers.Api.Core.Application.Validators
{
    public class CreateCustomerRequestValidator : ModelValidatorBase<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.DateOfBirth).InclusiveBetween(new DateTime(1900, 1, 1), DateTime.Now);
        }
    }
}