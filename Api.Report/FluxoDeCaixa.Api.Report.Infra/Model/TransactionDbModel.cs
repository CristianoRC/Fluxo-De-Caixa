using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Infra.Model;

public class TransactionDbModel : ITransactionReport
{
    public TransactionDbModel()
    {
    }

    public TransactionDbModel(Transaction transaction)
    {
        Id = transaction.Id;
        Description = transaction.Description;
        TypeId = transaction.Type;
        TransactionAmount = transaction.Type == Debit ? decimal.Negate(transaction.TransactionAmount.Value) : transaction.TransactionAmount.Value;
        BalanceAfterTransaction = transaction.BalanceAfterTransaction.Value;
        BalanceName = transaction.Balance.Name;
        BalanceId = transaction.Balance.Id;
        CreatedAt = transaction.CreatedAt;
        TypeId = transaction.Type;
    }

    private const int Debit = 0;
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Type => TypeId == 0 ? "Débito" : "Crédito";
    public int TypeId { get; set; }
    public decimal TransactionAmount { get; set; }
    public decimal BalanceAfterTransaction { get; set; }
    public Guid BalanceId { get; set; }
    public string BalanceName { get; set; }
    public DateTime CreatedAt { get; set; }
}