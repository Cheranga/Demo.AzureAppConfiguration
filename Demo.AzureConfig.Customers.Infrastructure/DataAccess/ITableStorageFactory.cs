using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Demo.AzureConfig.Customers.Infrastructure.DataAccess
{
    public interface ITableStorageFactory
    {
        Task<CloudTable> GetTableAsync(string tableName);
    }
}