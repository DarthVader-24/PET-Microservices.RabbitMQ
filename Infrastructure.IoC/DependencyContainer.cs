using Banking.Application.Interfaces;
using Banking.Application.Services;
using Banking.Data.Context;
using Banking.Data.Repositories;
using Banking.Domain.CommandHandlers;
using Banking.Domain.Commands;
using Banking.Domain.Interfaces;
using Domain.Core.Bus;
using Infrastructure.Bus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Transfer.Application.Interfaces;
using Transfer.Application.Services;
using Transfer.Data.Repositories;
using Transfer.Domain.EventHandlers;
using Transfer.Domain.Events;
using Transfer.Domain.Interfaces;

namespace Infrastructure.IoC;

public static class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services, MicroservicesEnum microservicesEnum)
    {
        // Domain Bus
        services.AddTransient<IEventBus, RabbitMQBus>();

        switch (microservicesEnum)
        {
            case MicroservicesEnum.Banking:
                // Domain Banking Commands
                services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
                
                // Application Services
                services.AddScoped<IAccountService, AccountService>();

                // Data
                services.AddScoped<IAccountRepository, AccountRepository>();
                break;
            case MicroservicesEnum.Transfer:
                // Domain Events
                services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
                
                // Application Services
                services.AddScoped<ITransferService, TransferService>();

                // Data
                services.AddScoped<ITransferRepository, TransferRepository>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(microservicesEnum), microservicesEnum, null);
        }
    }
}