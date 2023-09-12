using FluxoDeCaixa.Domain.Services.BookEntry;

namespace FluxoDeCaixa.Application.Services.BookEntry.DistributedLock;

public interface IBookEntryLock
{
    Task Acquire(CreateBookEntry command);
    Task Release(CreateBookEntry command);
}