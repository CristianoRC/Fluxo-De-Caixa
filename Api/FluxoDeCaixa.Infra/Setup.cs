using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection service)
    {
        return service;
    }
}