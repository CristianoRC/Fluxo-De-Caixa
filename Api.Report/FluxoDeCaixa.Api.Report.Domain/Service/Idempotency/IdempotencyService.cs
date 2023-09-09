using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;

namespace FluxoDeCaixa.Api.Report.Domain.Service;

public class IdempotencyService : IIdempotencyService
{
    private readonly IIdempotencyRepository _repository;

    public IdempotencyService(IIdempotencyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> MessageAlreadyProcessed(Idempotency idempotency)
    {
        if (idempotency is null)
            return true;
        if (idempotency.IdempotencyKey == Guid.Empty)
            return true;

        return await _repository.AlreadyProcess(idempotency.IdempotencyKey);
    }
}