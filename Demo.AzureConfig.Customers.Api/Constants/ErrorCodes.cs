namespace Demo.AzureConfig.Customers.Api.Constants
{
    public class ErrorCodes
    {
        public const string CustomerNotFound = nameof(CustomerNotFound);
        public const string InvalidCustomerSearch = nameof(InvalidCustomerSearch);
        public const string SearchByCustomerIdDataError = nameof(SearchByCustomerIdDataError);
        public const string SearchCustomerError = nameof(SearchCustomerError);
    }

    public class ErrorMessages
    {
        public const string CustomerNotFound = "customer not found";
        public const string InvalidCustomerSearch = "invalid customer search request";
        public const string SearchByCustomerIdDataError = "error occured when searching customer by id";
        public const string SearchCustomerError = "error occured when searching for customer";
    }
}