using FluxoDeCaixa.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IBookEntryApplicationService, BookEntryApplicationService>();
        return service;
    }
}