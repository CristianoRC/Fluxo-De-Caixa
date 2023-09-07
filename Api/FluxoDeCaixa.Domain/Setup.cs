using FluxoDeCaixa.Domain.Services.BookEntry;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Domain;

public static class Setup
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {
        service.AddScoped<BookEntryService>();
        return service;
    }
}