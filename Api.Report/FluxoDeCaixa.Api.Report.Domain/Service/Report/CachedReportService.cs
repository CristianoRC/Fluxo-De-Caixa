using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class CachedReportService : IReportService
{
    private readonly IDistributedCache _cache;
    private readonly IReportService _reportServiceWithoutCache;

    public CachedReportService(IReportService reportServiceWithoutCache, IDistributedCache cache)
    {
        _cache = cache;
        _reportServiceWithoutCache = reportServiceWithoutCache;
    }

    public async Task<byte[]> GenerateReport(ReportQuery reportQuery)
    {
        var cacheKey = GenerateCacheKey(reportQuery);
        var cachedReport = await _cache.GetStringAsync(cacheKey);

        if (cachedReport != null)
        {
            return JsonSerializer.Deserialize<byte[]>(cachedReport)!;
        }

        var report = await _reportServiceWithoutCache.GenerateReport(reportQuery);

        var cacheOptions = new DistributedCacheEntryOptions();
        cacheOptions.SetAbsoluteExpiration(IsHistoricalReport(reportQuery)
            ? TimeSpan.FromDays(30)
            : TimeSpan.FromHours(1));
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(report), cacheOptions);
        return report;
    }

    private static string GenerateCacheKey(ReportQuery parameters)
    {
        return $"Report_{parameters.BalanceId}_{parameters.Date:yyyyMMdd}";
    }

    private static bool IsHistoricalReport(ReportQuery parameters)
    {
        return parameters.Date < DateOnly.FromDateTime(DateTime.Today);
    }
}