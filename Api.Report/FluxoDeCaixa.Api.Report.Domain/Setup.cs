using FluxoDeCaixa.Api.Report.Domain.Service;
using FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxoDeCaixa.Api.Report.Domain;

public static class Setup
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {
        service.AddScoped<IBookEntryService, BookEntryService>();
        service.AddScoped<IIdempotencyService, IdempotencyService>();
        service.AddScoped<ReportService>();
        service.AddSingleton<IReportService>(provider =>
        {
            var reportService = provider.GetRequiredService<ReportService>();
            var distributedCache = provider.GetRequiredService<IDistributedCache>();
            return new CachedReportService(reportService, distributedCache);
        });
        return service;
    }
}