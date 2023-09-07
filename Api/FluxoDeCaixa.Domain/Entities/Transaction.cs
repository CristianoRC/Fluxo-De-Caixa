using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Entities;

public class Transaction : IEntity
{
    public Transaction(TransactionType transactionType, Amount amount, Balance balance)
    {
        Id = Guid.NewGuid();
        Type = transactionType;
        Amount = amount;
        Balance = balance;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }
    public TransactionType Type { get; }
    public Amount Amount { get; }
    public Balance Balance { get; set; }
    public DateTimeOffset CreatedAt { get; }

    public bool IsValid => Balance.IsValid && Amount.IsValid;
}