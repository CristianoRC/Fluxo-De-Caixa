using System.Text;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportService : IReportService
{
    public async Task<byte[]> GenerateReport(ReportQuery reportQuery)
    {
        /*var text = await File.ReadAllTextAsync(@"/Users/cristianorc/Desktop/report.html", Encoding.UTF8);
        return Encoding.UTF8.GetBytes(text);*/
        throw new NotImplementedException();
    }
}