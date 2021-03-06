using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Infrastructure.Configs;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.AzureConfig.Customers.Infrastructure.DataAccess
{
    public class StorageTableFactory : ITableStorageFactory
    {
        private readonly CloudTableClient _tableClient;

        public StorageTableFactory(StorageTableConfiguration configuration)
        {
            var storageAccount = CloudStorageAccount.Parse(configuration.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return null;
            }

            var table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            return table;
        }
    }
}