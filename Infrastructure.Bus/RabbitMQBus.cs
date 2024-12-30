using System.Text;
using Domain.Core.Bus;
using Domain.Core.Commands;
using Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Bus;

public sealed class RabbitMQBus(IMediator? mediator, IServiceScopeFactory scopeFactory) : IEventBus
{
    private readonly Dictionary<string, List<Type>> _handlers = [];
    private readonly List<Type> _eventTypes = [];

    public Task? SendCommand<T>(T command) where T : Command
    {
        return mediator?.Send(command);
    }

    public async Task PublishAsync<T>(T @event) where T : Event
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        var eventName = @event.GetType().Name;

        await channel.QueueDeclareAsync(eventName, false, false, false);

        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync("", eventName, body);
    }

    public async Task SubscribeAsync<T, TH>() where T : Event where TH : IEventHandler<T>
    {
        var eventType = typeof(T);
        var handlerType = typeof(TH);

        if (!_eventTypes.Contains(eventType))
        {
            _eventTypes.Add(eventType);
        }

        if (!_handlers.TryGetValue(eventType.Name, out var value))
        {
            value = new List<Type>();
            _handlers.Add(eventType.Name, value);
        }

        if (value.Any(h => h == handlerType))
        {
            throw new ArgumentException($"Handler type {handlerType.Name} is already registered for '{eventType.Name}'",
                nameof(handlerType));
        }
        
        _handlers[eventType.Name].Add(handlerType);

        await StartBasicConsumeAsync<T>();
    }

    private async Task StartBasicConsumeAsync<T>() where T: Event
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        var eventName = typeof(T).Name;
        
        await channel.QueueDeclareAsync(eventName, false, false, false);
        
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += Consumer_Received;
        
        await channel.BasicConsumeAsync(eventName, true, consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        var eventName = e.RoutingKey;
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.TryGetValue(eventName, out var subscriptions))
        {
            using var scope = scopeFactory.CreateScope();
            
            foreach (var subscription in subscriptions)
            {
                var handler = scope.ServiceProvider.GetService(subscription);
                
                if (handler is null) continue;
                
                var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);

                if (eventType is null) continue;
                
                var @event = JsonConvert.DeserializeObject(message, eventType);
                    
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, [@event]);
            }
        }
    }
}