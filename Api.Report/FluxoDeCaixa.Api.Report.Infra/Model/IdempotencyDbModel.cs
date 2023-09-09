using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Infra.Model;

public class IdempotencyDbModel : Idempotency
{
    public IdempotencyDbModel()
    {
    }

    public IdempotencyDbModel(Guid idempotencyKey)
    {
        IdempotencyKey = idempotencyKey;
    }
    
    public Guid IdempotencyKey { get; set; }
}