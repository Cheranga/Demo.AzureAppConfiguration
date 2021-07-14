using System.Threading.Tasks;

namespace Demo.AzureConfig.Customers.Api.Core.Application.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command);
    }
}