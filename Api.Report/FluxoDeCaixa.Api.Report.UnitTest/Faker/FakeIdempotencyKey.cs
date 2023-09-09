using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.UnitTest.Faker;

public class FakeIdempotencyKey : Idempotency
{
    public Guid IdempotencyKey { get; set; }
}