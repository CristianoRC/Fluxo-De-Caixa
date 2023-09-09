using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Api.Report.Infra;

public static class Setup
{
    public static IServiceCollection AddInfra(this IServiceCollection service, IConfiguration configuration)
    {
        return service;
    }
}