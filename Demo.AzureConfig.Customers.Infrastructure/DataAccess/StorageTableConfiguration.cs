namespace Demo.AzureConfig.Customers.Infrastructure.DataAccess
{
    public class StorageTableConfiguration
    {
        public string ConnectionString { get; set; }
        public string CustomersTable { get; set; }
        public string ContactsTable { get; set; }
    }
}