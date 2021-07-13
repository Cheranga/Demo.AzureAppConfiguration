using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Queries
{
    public interface IQueryHandler<in TQuery, TData> where TQuery : IQuery where TData : class
    {
        Task<Result<TData>> ExecuteAsync(TQuery query);
    }
}