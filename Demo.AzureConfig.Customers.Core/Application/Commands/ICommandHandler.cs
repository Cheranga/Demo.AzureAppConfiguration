using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Domain;

namespace Demo.AzureConfig.Customers.Core.Application.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command);
    }
}