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
        var reportBuilder = new ReportBuilder();
        var reportHtml = reportBuilder
            .InsertReportDate(reportQuery.Date)
            .InsertBalanceName("Teste")
            .InsertCreditAmount(1)
            .InsertDebitAmount(2)
            .InsertTotalAmount(3)
            .InsertTransactions(transactions)
            .Build();

        throw new NotImplementedException();
    }
}