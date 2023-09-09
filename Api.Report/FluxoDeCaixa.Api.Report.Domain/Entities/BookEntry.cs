namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public record BookEntry : Idempotency
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