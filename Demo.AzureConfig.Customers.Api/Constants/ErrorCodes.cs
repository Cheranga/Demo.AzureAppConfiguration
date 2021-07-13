namespace Demo.AzureConfig.Customers.Api.Constants
{
    public class ErrorCodes
    {
        public const string CustomerNotFound = nameof(CustomerNotFound);
        public const string InvalidCustomerSearch = nameof(InvalidCustomerSearch);
    }

    public class ErrorMessages
    {
        public const string CustomerNotFound = "customer not found";
        public const string InvalidCustomerSearch = "invalid customer search request";
    }
}