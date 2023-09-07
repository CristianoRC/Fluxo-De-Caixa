using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Configuration;

namespace FluxoDeCaixa.Infra.Repositories;

public class BookEntryRepository: IBookEntryRepository
{
    private readonly FluxoDeCaixaDataContext _dataContext;

    public BookEntryRepository(FluxoDeCaixaDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task Save(BookEntry bookEntry)
    {
        await _dataContext.AddAsync(bookEntry);
        await _dataContext.SaveChangesAsync();
    }
}