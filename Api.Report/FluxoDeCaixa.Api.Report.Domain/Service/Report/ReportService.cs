using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public class ReportService : IReportService
{
    private readonly IBookEntryRepository _repository;
    private readonly IPdfRenderService _renderService;

    public ReportService(IBookEntryRepository repository, IPdfRenderService renderService)
    {
        _repository = repository;
        _renderService = renderService;
    }

    public async Task<byte[]> GenerateReport(ReportQuery reportQuery)
    {
        var transactions = await _repository.GetTransactions(reportQuery);
        var consolidatedStatement = new ConsolidatedStatement(transactions);
        var reportBuilder = new ReportBuilder();
        var reportHtml = reportBuilder
            .InsertReportDate(reportQuery.Date)
            .InsertBalanceName(consolidatedStatement.BalanceName)
            .InsertCreditAmount(consolidatedStatement.CreditAmount)
            .InsertDebitAmount(consolidatedStatement.DebitAmount)
            .InsertTotalAmount(consolidatedStatement.TotalAmount)
            .InsertTransactions(consolidatedStatement.Transactions)
            .Build();

        return await _renderService.Render(reportHtml);
    }
}