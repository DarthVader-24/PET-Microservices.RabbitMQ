using Domain.Core.Bus;
using Transfer.Domain.Events;
using Transfer.Domain.Interfaces;
using Transfer.Domain.Models;

namespace Transfer.Domain.EventHandlers;

public class TransferEventHandler(ITransferRepository transferRepository) : IEventHandler<TransferCreatedEvent>
{
    private readonly ITransferRepository _transferRepository = transferRepository;

    public Task Handle(TransferCreatedEvent @event)
    {
        _transferRepository.AddTransferLog(new TransferLog
        {
            AccountSourceId = @event.From,
            AccountTargetId = @event.To,
            Amount = @event.Amount
        });
        return Task.CompletedTask;
    }
}