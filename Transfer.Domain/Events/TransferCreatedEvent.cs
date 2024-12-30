using Domain.Core.Events;

namespace Transfer.Domain.Events;

public class TransferCreatedEvent(int from, int to, decimal amount) : Event
{
    public int From { get; } = from;
    public int To { get; } = to;
    public decimal Amount { get; } = amount;
}