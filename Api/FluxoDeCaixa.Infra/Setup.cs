using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Configuration;
using FluxoDeCaixa.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
}