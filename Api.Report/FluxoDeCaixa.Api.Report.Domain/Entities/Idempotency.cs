namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public interface Idempotency
{
    public string IdempotencyKey { get; }
}