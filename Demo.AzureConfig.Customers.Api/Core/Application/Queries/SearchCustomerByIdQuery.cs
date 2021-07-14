namespace Demo.AzureConfig.Customers.Api.Core.Application.Queries
{
    public class SearchCustomerByIdQuery : IQuery
    {
        public string CustomerId { get; set; }
    }
}