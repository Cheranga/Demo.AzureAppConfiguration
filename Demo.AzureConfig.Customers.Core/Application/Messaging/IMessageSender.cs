using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Core.Domain;

namespace Demo.AzureConfig.Customers.Core.Application.Messaging
{
    public interface IMessageSender
    {
        Task<Result> SendAsync<TMessage>(TMessage message) where TMessage : class;
    }
}