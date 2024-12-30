using Banking.Application.Interfaces;
using Banking.Data.Context;
using Banking.Domain.Interfaces;
using Domain.Core.Bus;
using Infrastructure.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Domain Bus
        services.AddTransient<IEventBus, RabbitMQBus>();
        
        // Application Services
        services.AddScoped<IAccountService, IAccountService>();
        
        // Data
        services.AddScoped<IAccountRepository, IAccountRepository>();
        services.AddScoped<BankingDbContext>();
    }
}