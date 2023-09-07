using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Services.BookEntry;

namespace FluxoDeCaixa.Application.Services;

public interface IBookEntryApplicationService
{
    Task<BookEntry> Create(CreateBookEntry command);
}