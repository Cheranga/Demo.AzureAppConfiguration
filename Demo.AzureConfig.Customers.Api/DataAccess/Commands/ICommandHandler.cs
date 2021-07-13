using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;

namespace Demo.AzureConfig.Customers.Api.DataAccess.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command);
    }
}