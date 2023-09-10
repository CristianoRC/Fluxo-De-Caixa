namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public class ConsolidatedStatement
{
    public ConsolidatedStatement(IEnumerable<ITransactionReport> transactionReports)
    {
        Transactions = transactionReports;
        DebitAmount = Transactions.Where(x => x.TypeId == (int) TransactionType.Debit).Sum(x => x.TransactionAmount);
        CreditAmount = Transactions.Where(x => x.TypeId == (int) TransactionType.Credit).Sum(x => x.TransactionAmount);
        var firstTransaction = Transactions.FirstOrDefault();
        if (firstTransaction is not null)
            BalanceName = firstTransaction.BalanceName;
    }

    public decimal CreditAmount { get; }
    public decimal DebitAmount { get; }
    public decimal TotalAmount => CreditAmount - Math.Abs(DebitAmount);
    public string BalanceName { get; } = string.Empty;
    public IEnumerable<ITransactionReport> Transactions { get; }
}