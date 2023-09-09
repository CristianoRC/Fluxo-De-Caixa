namespace FluxoDeCaixa.Api.Report.UnitTest;

[Trait("Category", "Unit")]
public abstract class BaseUnitTest
{
    protected Bogus.Faker Faker => new ("pt_BR");
}