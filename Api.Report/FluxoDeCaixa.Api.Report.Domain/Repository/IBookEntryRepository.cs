using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Service.Report;

namespace FluxoDeCaixa.Api.Report.Domain.Repository;

public interface IBookEntryRepository
{
    Task SaveTransaction(IEnumerable<Transaction> transactions);
    Task<IEnumerable<ITransactionReport>> GetTransactions(ReportQuery reportQuery);
}