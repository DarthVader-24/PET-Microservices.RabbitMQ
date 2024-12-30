using System.Windows.Input;
using Domain.Core.Commands;
using Domain.Core.Events;

namespace Domain.Core.Bus;

public interface IEventBus
{
    Task? SendCommand<T>(T command) where T : Command;
    
    Task PublishAsync<T>(T @event) where T : Event;

    Task SubscribeAsync<T, TH>() where T : Event where TH : IEventHandler<T>;
}