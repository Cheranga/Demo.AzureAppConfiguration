using System;
using Demo.AzureConfig.Customers.Core.Application.Requests;
using Demo.AzureConfig.Customers.Core.Domain;
using FluentValidation;

namespace Demo.AzureConfig.Customers.Core.Application.Validators
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