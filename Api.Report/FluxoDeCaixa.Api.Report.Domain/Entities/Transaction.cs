namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public record Transaction
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public Amount TransactionAmount { get; set; }
    public Amount BalanceAfterTransaction { get; set; }
    public Balance Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
}