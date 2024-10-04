using FluxoDeCaixa.Api.Report.Domain;
using FluxoDeCaixa.Api.Report.Infra;
using Microsoft.Extensions.Hosting;

var hostBuilder = new HostBuilder()
    .ConfigureServices((builderContext, services) =>
    {
        services
            .AddDomain()
            .AddInfra(builderContext.Configuration, builderContext.HostingEnvironment.IsDevelopment());
    })
    .ConfigureFunctionsWorkerDefaults();

var host = hostBuilder.Build();
host.Run();