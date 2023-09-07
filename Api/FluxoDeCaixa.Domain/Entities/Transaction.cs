using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Entities;

public class Transaction : IEntity
{
    protected Transaction()
    {
    }

    public Transaction(TransactionType transactionType, TransactionAmount transactionAmount, Balance balance)
    {
        Id = Guid.NewGuid();
        Type = transactionType;
        TransactionAmount = transactionAmount;
        Balance = balance;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdateBalanceAfterTransaction();
    }

    public Guid Id { get; protected set; }
    public TransactionType Type { get; protected set; }
    public TransactionAmount TransactionAmount { get; protected set; }
    public BalanceAmount BalanceAfterTransaction { get; protected set; }
    public Balance Balance { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public bool IsValid => Balance.IsValid && TransactionAmount.IsValid;

    private void UpdateBalanceAfterTransaction()
    {
        if (IsValid is false)
            return;
        var currentBalanceAmount = Balance.Amount.Value;
        var transactionAmount = Type == TransactionType.Credit
            ? TransactionAmount.Value
            : decimal.Negate(TransactionAmount.Value);
        BalanceAfterTransaction = new BalanceAmount(currentBalanceAmount + transactionAmount);
    }
}