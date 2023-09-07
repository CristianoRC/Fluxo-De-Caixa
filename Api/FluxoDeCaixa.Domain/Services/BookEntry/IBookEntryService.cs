namespace FluxoDeCaixa.Domain.Services.BookEntry;

public interface IBookEntryService
{
    Task<Aggregations.BookEntry> Create(CreateBookEntry command);
}