using Domain.Core.Bus;
using Transfer.Domain.Events;

namespace Transfer.Domain.EventHandlers;

public class TransferEventHandler: IEventHandler<TransferCreatedEvent>
{
    public TransferEventHandler()
    {
        
    }
    
    public Task Handle(TransferCreatedEvent @event)
    {
        return Task.CompletedTask;
    }
}