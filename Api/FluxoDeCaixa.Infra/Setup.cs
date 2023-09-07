using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Configuration;
using FluxoDeCaixa.Infra.MessageBus.Configuration;
using FluxoDeCaixa.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace FluxoDeCaixa.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration config)
    {
        service.AddScoped<IBookEntryRepository, BookEntryRepository>();
        service.AddScoped<IBalanceRepository, BalanceRepository>();

        service.AddDbContext<FluxoDeCaixaDataContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Sql")));
        
        return service;
    }
    
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConnection = ConfigureConnection(configuration);
        services.AddSingleton(rabbitMqConnection);
        services.AddScoped<ISendMessageService, SendMessageService>();
        return services;
    }

    private static IConnection ConfigureConnection(IConfiguration configuration)
    {
        var connectionFactory = new ConnectionRabbitMq(configuration.GetConnectionString("RabbitMQ"));
        var connection = connectionFactory.GetConnection();
        return connection;
    }
}