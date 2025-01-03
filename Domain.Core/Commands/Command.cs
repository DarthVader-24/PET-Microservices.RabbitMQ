using Domain.Core.Events;

namespace Domain.Core.Commands;

public abstract class Command: Message
{
    public DateTime Timestamp { get; protected set; } = DateTime.Now;
}