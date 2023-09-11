using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Domain.Service.Report;

public interface IReportBuilder
{
    IReportBuilder InsertReportDate(DateOnly reportDate);
    IReportBuilder InsertBalanceName(string balance);
    IReportBuilder InsertDebitAmount(decimal amount);
    IReportBuilder InsertCreditAmount(decimal amount);
    IReportBuilder InsertTotalAmount(decimal amount);
    IReportBuilder InsertTransactions(IEnumerable<ITransactionReport> transactions);
    string Build();
}