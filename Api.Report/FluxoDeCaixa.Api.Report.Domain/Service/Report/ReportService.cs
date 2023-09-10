using System.Text;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportService : IReportService
{
    public async Task<byte[]> GenerateReport(ReportQuery reportQuery)
    {
        //return await File.ReadAllBytesAsync(@"/Users/cristianorc/Desktop/report.pdf");
        throw new NotImplementedException();
    }
}