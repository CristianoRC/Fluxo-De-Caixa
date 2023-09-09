using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;

namespace FluxoDeCaixa.Api.Report.Domain.Service;

public class IdempotencyService : IIdempotencyService
{
    public IIdempotencyRepository Repository { get; }

    public IdempotencyService(IIdempotencyRepository repository)
    {
        Repository = repository;
    }

    public async Task<bool> MessageAlreadyProcessed(Idempotency idempotency)
    {
        throw new NotImplementedException();
    }
}