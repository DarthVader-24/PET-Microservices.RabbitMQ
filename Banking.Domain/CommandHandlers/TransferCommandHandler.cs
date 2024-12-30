using Banking.Domain.Commands;
using Banking.Domain.Events;
using Domain.Core.Bus;
using MediatR;

namespace Banking.Domain.CommandHandlers;

public class TransferCommandHandler(IEventBus eventBus) : IRequestHandler<TransferCommand, bool>
{
    private readonly IEventBus _eventBus = eventBus;

    public Task<bool> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        _eventBus.PublishAsync(new TransferCreatedEvent(request.From, request.To, request.Amount));
        
        return Task.FromResult(true);
    }
}