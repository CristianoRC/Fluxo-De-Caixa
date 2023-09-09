namespace FluxoDeCaixa.Api.Report.Domain.Entities;

public interface IIDempotency
{
    public string IdempotencyKey { get; }

}