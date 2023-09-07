using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Services.BookEntry;

namespace FluxoDeCaixa.Application.Services;

public class BookEntryApplicationService : IBookEntryApplicationService
{
    private readonly IBookEntryService _bookEntryService;

    public BookEntryApplicationService(IBookEntryService bookEntryService)
    {
        _bookEntryService = bookEntryService;
    }
    
    public async Task<BookEntry> Create(CreateBookEntry command)
    {
        throw new NotImplementedException();
    }
}