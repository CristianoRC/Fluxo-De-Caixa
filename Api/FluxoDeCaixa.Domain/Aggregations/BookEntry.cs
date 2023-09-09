using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Aggregations;

public class BookEntry : IAggregate
{
    protected BookEntry(){ }
    
    public BookEntry(TransactionAmount transactionAmount, Balance entryBalance, Balance offsetBalance, TransactionType entryTransactionType, string description)
    {
        ArgumentNullException.ThrowIfNull(transactionAmount);

        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
        Errors = GetErrors(transactionAmount, entryBalance, offsetBalance);
        if (Errors.Any())
            return;
        Entry = new Transaction(entryTransactionType, transactionAmount, entryBalance, description);
        Offset = new Transaction(GetOffsetTransactionType(entryTransactionType), transactionAmount, offsetBalance, description);
    }

    public Guid Id { get; protected set;}
    public Transaction Entry { get; protected set; }
    public Transaction Offset { get; protected set;}
    public IEnumerable<string> Errors { get; } = Enumerable.Empty<string>();
    public DateTimeOffset CreatedAt { get; protected set;}
    
    private static TransactionType GetOffsetTransactionType(TransactionType entryTransactionType)
    {
        return entryTransactionType is TransactionType.Credit ? TransactionType.Debit : TransactionType.Credit;
    }

    private static IEnumerable<string> GetErrors(TransactionAmount transactionAmount, Balance? entryBalance, Balance? offsetBalance)
    {
        if (transactionAmount.IsValid is false)
            yield return "Valor do amount inválido";

        if (entryBalance is null || entryBalance.IsValid is false)
            yield return "Balance de partida inválido";

        if (offsetBalance is null || offsetBalance.IsValid is false)
            yield return "Balance de contrapartida inválido";

        if (entryBalance is null || offsetBalance is null) yield break;
        if (entryBalance == offsetBalance)
            yield return "Balance duplicado";
    }
}