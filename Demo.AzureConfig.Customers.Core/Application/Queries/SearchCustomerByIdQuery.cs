namespace Demo.AzureConfig.Customers.Core.Application.Queries
{
    public class SearchCustomerByIdQuery : IQuery
    {
        public string CustomerId { get; set; }
    }
}