namespace Demo.AzureConfig.Customers.Api.DataAccess.Queries
{
    public class SearchCustomerByIdQuery : IQuery
    {
        public string CustomerId { get; set; }
    }
}