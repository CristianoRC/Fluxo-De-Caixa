using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Domain.Service;

public interface IIdempotencyService
{
    Task<bool> MessageAlreadyProcessed(Idempotency idempotency);
    Task<bool> MarkAsProcessed(Idempotency idempotency);
}