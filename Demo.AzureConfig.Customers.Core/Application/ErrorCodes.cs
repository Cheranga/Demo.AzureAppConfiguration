namespace Demo.AzureConfig.Customers.Core.Application
{
    public class ErrorCodes
    {
        public const string CustomerNotFound = nameof(CustomerNotFound);
        public const string InvalidCustomerSearch = nameof(InvalidCustomerSearch);
        public const string SearchByCustomerIdDataError = nameof(SearchByCustomerIdDataError);
        public const string SearchCustomerError = nameof(SearchCustomerError);
        public const string CreateCustomerDataError = nameof(CreateCustomerDataError);
        public const string InvalidCreateCustomerRequest = nameof(InvalidCreateCustomerRequest);
        public const string MessagePublishError = nameof(MessagePublishError);
    }

    public class ErrorMessages
    {
        public const string CustomerNotFound = "customer not found";
        public const string InvalidCustomerSearch = "invalid customer search request";
        public const string SearchByCustomerIdDataError = "error occured when searching customer by id";
        public const string SearchCustomerError = "error occured when searching for customer";
        public const string CreateCustomerDataError = "error occured when creating the customer";
        public const string InvalidCreateCustomerRequest = "invalid create customer request";
        public const string MessagePublishError = "error occured when publishing message to the service bus";
    }
}