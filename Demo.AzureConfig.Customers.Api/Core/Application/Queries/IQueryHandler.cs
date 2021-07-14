using System.Threading.Tasks;

namespace Demo.AzureConfig.Customers.Api.Core.Application.Queries
{
    public interface IQueryHandler<in TQuery, TData> where TQuery : IQuery where TData : class
    {
        Task<Result<TData>> ExecuteAsync(TQuery query);
    }
}