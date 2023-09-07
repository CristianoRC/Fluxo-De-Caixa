using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Repositories;

namespace FluxoDeCaixa.Infra.Repositories;

public class BookEntryRepository: IBookEntryRepository
{
    public Task Save(BookEntry bookEntry)
    {
        throw new NotImplementedException();
    }
}