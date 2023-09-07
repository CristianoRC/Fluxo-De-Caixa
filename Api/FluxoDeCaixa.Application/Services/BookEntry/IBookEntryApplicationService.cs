using FluxoDeCaixa.Domain.Services.BookEntry;

namespace FluxoDeCaixa.Application.Services.BookEntry;

public interface IBookEntryApplicationService
{
    Task<Domain.Aggregations.BookEntry> Create(CreateBookEntry command);
}