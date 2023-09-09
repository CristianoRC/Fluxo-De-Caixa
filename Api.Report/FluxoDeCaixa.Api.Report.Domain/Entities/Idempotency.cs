namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public interface Idempotency
{
    public Guid IdempotencyKey { get; }
}