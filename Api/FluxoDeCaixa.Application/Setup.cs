using FluxoDeCaixa.Application.Services;
using FluxoDeCaixa.Application.Services.BookEntry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace FluxoDeCaixa.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration config)
    {
        service.AddScoped<IBookEntryApplicationService, BookEntryApplicationService>();
        
        var endPoints = new List<RedLockMultiplexer>
        {
            ConnectionMultiplexer.Connect(config.GetConnectionString("Redis"))
        };
        var redlockFactory = RedLockFactory.Create(endPoints);
        service.AddSingleton<IDistributedLockFactory>(redlockFactory);
        
        return service;
    }
}