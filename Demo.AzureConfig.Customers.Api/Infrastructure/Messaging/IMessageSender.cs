using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Core;

namespace Demo.AzureConfig.Customers.Api.Infrastructure.Messaging
{
    public interface IMessageSender
    {
        Task<Result> SendAsync<TMessage>(TMessage message) where TMessage : class;
    }
}