using Domain.Core.Bus;
using Domain.Core.Commands;
using Domain.Core.Events;

namespace Infrastructure.Bus;

public sealed class RabbitMQBus: IEventBus
{
    public Task SendCommand<T>(T command) where T : Command
    {
        throw new NotImplementedException();
    }

    public void Publish<T>(T @event) where T : Event
    {
        throw new NotImplementedException();
    }

    public void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>
    {
        throw new NotImplementedException();
    }
}