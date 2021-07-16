using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Domain;

namespace Demo.AzureConfig.Customers.Core.Application.Queries
{
    public interface IQueryHandler<in TQuery, TData> where TQuery : IQuery where TData : class
    {
        Task<Result<TData>> ExecuteAsync(TQuery query);
    }
}