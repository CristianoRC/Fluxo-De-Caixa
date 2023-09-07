using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.Domain.Entities;

public class Transaction : IEntity
{
    public Transaction(Guid id, TransactionType type, TransactionAmount transactionAmount, BalanceAmount balanceAfterTransaction, Balance balance, DateTimeOffset createdAt)
    {
        Id = id;
        Type = type;
        TransactionAmount = transactionAmount;
        BalanceAfterTransaction = balanceAfterTransaction;
        Balance = balance;
        CreatedAt = createdAt;  
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

    public Guid Id { get; }
    public TransactionType Type { get; }
    public TransactionAmount TransactionAmount { get; }
    public BalanceAmount BalanceAfterTransaction { get; private set; }
    public Balance Balance { get; }
    public DateTimeOffset CreatedAt { get; }
    public bool IsValid => Balance.IsValid && TransactionAmount.IsValid;

    private void UpdateBalanceAfterTransaction()
    {
        if (IsValid is false)
            return;
        var currentBalanceAmount = Balance.Amount.Value;
        var transactionAmount = Type == TransactionType.Credit ? TransactionAmount.Value : decimal.Negate(TransactionAmount.Value);
        BalanceAfterTransaction = new BalanceAmount(currentBalanceAmount + transactionAmount);
    }
}