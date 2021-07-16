namespace Demo.AzureConfig.Customers.Infrastructure.Configs
{
    public class StorageTableConfiguration
    {
        public string ConnectionString { get; set; }
        public string CustomersTable { get; set; }
        public string ContactsTable { get; set; }
    }
}