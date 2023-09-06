using Bogus;

namespace FluxoDeCaixa.UnitTest;

[Trait("Category", "Unit")]
public abstract class BaseUnitTest
{
    protected Faker Faker => new Faker("pt_BR");
}