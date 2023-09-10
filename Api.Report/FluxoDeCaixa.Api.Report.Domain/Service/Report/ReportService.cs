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
        //TODO: Implementar logica de onde pegar esses dados
        //var transactions = await _repository.GetTransactions(reportQuery);
        var reportBuilder = new ReportBuilder();
        var reportHtml = reportBuilder
            .InsertReportDate(reportQuery.Date)
            .InsertBalanceName("Teste")
            .InsertCreditAmount(1)
            .InsertDebitAmount(2)
            .InsertTotalAmount(3)
            .InsertTransactions(Enumerable.Empty<ITransactionReport>())
            .Build();

        return await _renderService.Render(reportHtml);
    }
}