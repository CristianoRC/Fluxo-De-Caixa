namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public interface ITransactionReport
{
    public Guid Id { get; }
    public string Description { get; }
    public string Type { get; }
    public int TypeId { get; }
    public decimal TransactionAmount { get; }
    public decimal BalanceAfterTransaction { get; }
    public Guid BalanceId { get; }
    public string BalanceName { get; }
    public DateTime CreatedAt { get; }
}