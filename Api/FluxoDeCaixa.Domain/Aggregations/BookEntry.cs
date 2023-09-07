using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Aggregations;

public class BookEntry : IAggregate
{
    public BookEntry(Amount amount, Balance entryBalance, Balance offsetBalance, TransactionType entryTransactionType)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }
    public Transaction Entry { get; set; }
    public Transaction Offset { get; set; }
    public IEnumerable<string> Errors { get; }
    public DateTimeOffset CreatedAt { get; set; }
}