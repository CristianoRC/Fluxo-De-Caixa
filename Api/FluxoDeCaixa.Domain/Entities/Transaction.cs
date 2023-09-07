using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Entities;

public class Transaction : IEntity
{
    public Transaction(TransactionType  transactionType, Amount amount)
    {
        Id = Guid.NewGuid();
        Type = transactionType;
    }

    public Guid Id { get; }

    public TransactionType Type { get; }
}