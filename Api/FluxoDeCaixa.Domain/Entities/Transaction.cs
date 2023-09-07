using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Entities;

public class Transaction : IEntity
{
    public Transaction(TransactionType transactionType, TransactionAmount transactionAmount, Balance balance)
    {
        Id = Guid.NewGuid();
        Type = transactionType;
        TransactionAmount = transactionAmount;
        Balance = balance;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }
    public TransactionType Type { get; }
    public TransactionAmount TransactionAmount { get; }
    public Balance Balance { get; set; }
    public DateTimeOffset CreatedAt { get; }

    public bool IsValid => Balance.IsValid && TransactionAmount.IsValid;
}