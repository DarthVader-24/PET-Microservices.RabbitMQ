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

namespace Infrastructure.IoC;

public static class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Domain Bus
        services.AddTransient<IEventBus, RabbitMQBus>();
        
        // Domain Banking Commands
        services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
        
        // Application Services
        services.AddScoped<IAccountService, AccountService>();
        
        // Data
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}