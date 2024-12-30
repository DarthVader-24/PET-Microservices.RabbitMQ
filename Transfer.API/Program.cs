using Domain.Core.Bus;
using Transfer.Data.Context;
using Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared;
using Transfer.Domain.EventHandlers;
using Transfer.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TransferDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("TransferDbConnection"))
);

DependencyContainer.RegisterServices(builder.Services, MicroservicesEnum.Transfer);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transfer Microservice",
        Version = "v1"
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer Microservice"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ConfigureEventBus(app);

app.Run();

return;

void ConfigureEventBus(IApplicationBuilder applicationBuilder)
{
    var eventBus = applicationBuilder.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.SubscribeAsync<TransferCreatedEvent, TransferEventHandler>();
}

