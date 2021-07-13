using FluentValidation.Results;

namespace Demo.AzureConfig.Customers.Api.Core
{
    public class Result<TData> where TData:class
    {
        public string ErrorCode { get; set; }
        public ValidationResult ValidationResult { get; set; }
        public TData Data { get; set; }

        public bool Status => string.IsNullOrWhiteSpace(ErrorCode);

        public static Result<TData> Success(TData data)
        {
            return new Result<TData>
            {
                Data = data
            };
        }

        public static Result<TData> Failure(string errorCode, string errorMessage)
        {
            var validationResult = new ValidationResult(new[]
            {
                new ValidationFailure("", errorMessage)
                {
                    ErrorCode = errorCode
                }
            });

            return Failure(errorCode, validationResult);
        }

        public static Result<TData> Failure(string errorCode, ValidationResult validationResult)
        {
            return new Result<TData>
            {
                ErrorCode = errorCode,
                ValidationResult = validationResult
            };
        }
    }
}