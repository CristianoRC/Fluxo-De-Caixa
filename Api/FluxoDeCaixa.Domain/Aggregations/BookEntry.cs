using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Aggregations;

public class BookEntry : IAggregate
{
    public BookEntry(TransactionAmount transactionAmount, Balance entryBalance, Balance offsetBalance, TransactionType entryTransactionType)
    {
        ArgumentNullException.ThrowIfNull(transactionAmount);
        ArgumentNullException.ThrowIfNull(entryBalance);
        ArgumentNullException.ThrowIfNull(offsetBalance);
        
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        Entry = new Transaction(entryTransactionType, transactionAmount, entryBalance);
        Offset = new Transaction(GetOffsetTransactionType(entryTransactionType), transactionAmount, offsetBalance);
        Errors = GetErrors(transactionAmount, entryBalance, offsetBalance);
    }

    public Guid Id { get; }
    public Transaction Entry { get; }
    public Transaction Offset { get; }
    public IEnumerable<string> Errors { get; }
    public DateTimeOffset CreatedAt { get; }

    private static TransactionType GetOffsetTransactionType(TransactionType entryTransactionType)
    {
        return entryTransactionType is TransactionType.Credit ? TransactionType.Debit : TransactionType.Credit;
    }

    private static IEnumerable<string> GetErrors(TransactionAmount transactionAmount, Balance entryBalance, Balance offsetBalance)
    {
        if (transactionAmount.IsValid is false)
            yield return "Valor do amount inválido";

        if (entryBalance == offsetBalance)
            yield return "Balance duplicado";

        if (entryBalance.IsValid is false)
            yield return "Balance de partida inválido";

        if (offsetBalance.IsValid is false)
            yield return "Balance de contrapartida inválido";
    }
}