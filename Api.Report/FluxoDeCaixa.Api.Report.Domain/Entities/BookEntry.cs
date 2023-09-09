namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public record BookEntry : Idempotency
{
    public Guid IdempotencyKey { get; set; }
    public Guid CorrelationId { get; set; }
    public BookEntryData BookEntryData { get; set; }
}

public record BookEntryData
{
    public Guid Id { get; set; }
    public Transaction Entry { get; set; }
    public Transaction Offset { get; set; }
    public DateTime CreatedAt { get; set; }
}