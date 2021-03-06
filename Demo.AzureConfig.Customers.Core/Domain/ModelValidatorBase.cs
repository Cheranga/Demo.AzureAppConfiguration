using FluentValidation;
using FluentValidation.Results;

namespace Demo.AzureConfig.Customers.Core.Domain
{
    public class ModelValidatorBase<TModel> : AbstractValidator<TModel> where TModel : class
    {
        protected ModelValidatorBase()
        {
            CascadeMode = CascadeMode.Stop;
        }

        protected override bool PreValidate(ValidationContext<TModel> context, ValidationResult result)
        {
            var instance = context.InstanceToValidate;
            if (instance != null)
            {
                return true;
            }

            result.Errors.Add(new ValidationFailure("", "instance is null"));
            return false;
        }
    }
}