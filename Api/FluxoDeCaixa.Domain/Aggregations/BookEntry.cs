using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Aggregations;

public class BookEntry : IAggregate
{
    public BookEntry(Amount amount, Balance entryBalance, Balance offsetBalance, TransactionType entryTransactionType)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        Entry = new Transaction(entryTransactionType, amount, entryBalance);
        Offset = new Transaction(GetOffsetTransactionType(entryTransactionType), amount, offsetBalance);
    }

    public Guid Id { get; }
    public Transaction Entry { get; }
    public Transaction Offset { get; }
    public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();
    public DateTimeOffset CreatedAt { get; }

    private static TransactionType GetOffsetTransactionType(TransactionType entryTransactionType)
    {
        return entryTransactionType is TransactionType.Credit ? TransactionType.Debit : TransactionType.Credit;
    }
}