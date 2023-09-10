using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportService : IReportService
{
    private readonly IBookEntryRepository _repository;

    public ReportService(IBookEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> GenerateReport(ReportQuery reportQuery)
    {
        var transactions = await _repository.GetTransactions(reportQuery);
        
        //return await File.ReadAllBytesAsync(@"/Users/cristianorc/Desktop/report.pdf");
        throw new NotImplementedException();
    }
}