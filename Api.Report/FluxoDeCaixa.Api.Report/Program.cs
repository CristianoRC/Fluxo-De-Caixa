using FluxoDeCaixa.Api.Report.Domain;
using Microsoft.Extensions.Hosting;

var hostBuilder = new HostBuilder()
    .ConfigureServices((builderContext, services) => { services.AddDomain(); })
    .ConfigureFunctionsWorkerDefaults();

var host = hostBuilder.Build();
host.Run();