using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        return service;
    }
}