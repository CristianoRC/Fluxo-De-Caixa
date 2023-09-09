using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Domain.Repository;

public interface IBookEntryRepository
{
    Task SaveTransaction(IEnumerable<Transaction> transactions);
}