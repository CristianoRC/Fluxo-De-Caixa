using Bogus;

namespace FluxoDeCaixa.Api.Report.UnitTest;

[Trait("Category", "Unit")]
public abstract class BaseUnitTest
{
    protected Faker Faker => new Faker("pt_BR");
}