using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.AzureConfig.Customers.Api.Constants;
using Demo.AzureConfig.Customers.Api.Core;
using Demo.AzureConfig.Customers.Api.DataAccess.Commands;
using Demo.AzureConfig.Customers.Api.Messaging;
using FluentValidation;
using MediatR;

namespace Demo.AzureConfig.Customers.Api.Services.Requests
{
    public class CreateCustomerRequest : IRequest<Result>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Result>
    {
        private readonly IValidator<CreateCustomerRequest> _validator;
        private readonly ICommandHandler<CreateCustomerCommand> _commandHandler;
        private readonly IMessageSender _messageSender;

        public CreateCustomerRequestHandler(IValidator<CreateCustomerRequest> validator, ICommandHandler<CreateCustomerCommand> commandHandler, IMessageSender messageSender)
        {
            _validator = validator;
            _commandHandler = commandHandler;
            _messageSender = messageSender;
        }
        
        public async Task<Result> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Failure(ErrorCodes.InvalidCreateCustomerRequest, ErrorMessages.InvalidCreateCustomerRequest);
            }

            var command = new CreateCustomerCommand
            {
                Id = Guid.NewGuid().ToString("N").ToUpper(),
                Name = request.Name,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth
            };

            var operation = await _commandHandler.ExecuteAsync(command);
            if (!operation.Status)
            {
                return operation;
            }
            
            var customerCreatedEvent = new CustomerCreatedEvent
            {
                Id = command.Id,
                CreatedOn = DateTime.UtcNow
            };
            
            operation = await _messageSender.SendAsync(customerCreatedEvent);
            
            return operation;
        }
    }
}