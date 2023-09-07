using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Services.BookEntry;

public record CreateBookEntry
{
    public Guid EntryBalance { get; set; }
    public Guid OffsetBalance { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
}