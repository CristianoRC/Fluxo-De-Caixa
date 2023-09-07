using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.UnitTest.ValueObjects;

public class AmountUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Deve ser inválido caso o valor do amount seja negativo")]
    public void NegativeAmount()
    {
        //Arrange
        var value = Faker.Finance.Amount(max: decimal.Zero - 1);

        //Act
        var amount = new Amount(value);

        //Assert
        amount.IsValid.Should().BeFalse();
        amount.Value.Should().Be(value);
    }

    [Fact(DisplayName = "Deve ser válido caso o valor do amount seja Zero")]
    public void ZeroAmount()
    {
        //Arrange
        const decimal value = decimal.Zero;

        //Act
        var amount = new Amount(value);

        //Assert
        amount.IsValid.Should().BeTrue();
        amount.Value.Should().Be(decimal.Zero);
    }

    [Fact(DisplayName = "Deve ser válido caso o valor do amount seja positivo")]
    public void PositiveAmount()
    {
        //Arrange
        var value = Faker.Finance.Amount(min: 0.00000001m);

        //Act
        var amount = new Amount(value);

        //Assert
        amount.IsValid.Should().BeTrue();
        amount.Value.Should().Be(value);
    }
}