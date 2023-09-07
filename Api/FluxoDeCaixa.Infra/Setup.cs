using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection service)
    {
        service.AddScoped<IBookEntryRepository, BookEntryRepository>();
        service.AddScoped<IBalanceRepository, BalanceRepository>();

        return service;
    }
}