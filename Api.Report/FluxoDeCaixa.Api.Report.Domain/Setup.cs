using FluxoDeCaixa.Api.Report.Domain.Service;
using FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Api.Report.Domain;

public static class Setup
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {
        service.AddScoped<IBookEntryService, BookEntryService>();
        service.AddScoped<IIdempotencyService, IdempotencyService>();

        return service;
    }
}