using FluxoDeCaixa.Domain.Aggregations;

namespace FluxoDeCaixa.Domain.Repositories;

public interface IBookEntryRepository
{
    public Task Save(BookEntry bookEntry);
}