namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public interface IReportService
{
    public Task<byte[]> GenerateReport(ReportQuery reportQuery);
}