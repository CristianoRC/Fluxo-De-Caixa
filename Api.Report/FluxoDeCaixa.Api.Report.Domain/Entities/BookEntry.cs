namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public record BookEntry : IIDempotency
{
    public string IdempotencyKey { get; set; }
    public string CorrelationId { get; set; }
    public BookEntryData BookEntryData { get; set; }
}

public record BookEntryData
{
    public Guid Id { get; set; }
    public Transaction Entry { get; set; }
    public Transaction Offset { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record Transaction
{
    public string Id { get; set; }
    public int Type { get; set; }
    public Amount TransactionAmount { get; set; }
    public Amount BalanceAfterTransaction { get; set; }
    public Balance Balance { get; set; }
    public DateTime CreatedAt { get; set; }
}