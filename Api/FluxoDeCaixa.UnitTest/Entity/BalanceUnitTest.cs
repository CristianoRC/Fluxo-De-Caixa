using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.ValueObjects;

namespace FluxoDeCaixa.UnitTest.Entity;

public class BalanceUnitTest : BaseUnitTest
{
    [Theory(DisplayName = "Deve ser considerado inválido caso o nome seja null ou em branco")]
    [InlineData("")]
    [InlineData(null)]
    public void InvalidName(string balanceName)
    {
        //Arrange/Act
        var balance = new Balance(balanceName);

        //Assert
        balance.IsValid.Should().BeFalse();
    }

    [Fact(DisplayName = "Ao passar o nome, o balance deve ser considerado válido")]
    public void ValidBalance()
    {
        //Arrange
        var name = Faker.Finance.AccountName();

        //Act
        var balance = new Balance(name);

        //Assert
        balance.IsValid.Should().BeTrue();
        balance.Name.Should().Be(name);
        balance.Id.Should().NotBe(Guid.Empty);
        balance.CreatedAt.Should().NotBe(default);
    }

    [Fact(DisplayName = "Ao criar um novo balance, seu amount deve estar zerado")]
    public void BalanceAmount()
    {
        //Arrange
        var name = Faker.Finance.AccountName();

        //Act
        var balance = new Balance(name);

        //Assert
        balance.Amount.Value.Should().Be(decimal.Zero);
        
    }
    
    [Fact(DisplayName = "Balances devem ser considerados iguais se seus ids forem os mesmo")]
    public void TheSameId()
    {
        //Arrange
        var id = Guid.NewGuid();
        var firstBalance = new Balance(id, Faker.Random.Word(), Faker.Date.PastOffset(), GenerateRandomBalanceAmount());
        var secondBalance = new Balance(id, Faker.Random.Word(), Faker.Date.PastOffset(), GenerateRandomBalanceAmount());

        //Act
        var balancesAreEqual = firstBalance == secondBalance;

        //Assert
        balancesAreEqual.Should().BeTrue();
    }

    [Fact(DisplayName = "Balances devem ser considerados diferentes se seus ids não forem os mesmo")]
    public void DifferentId()
    {
        //Arrange
        var firstBalance = new Balance(Guid.NewGuid(), Faker.Random.Word(), Faker.Date.PastOffset(), GenerateRandomBalanceAmount());
        var secondBalance = new Balance(Guid.NewGuid(), Faker.Random.Word(), Faker.Date.PastOffset(), GenerateRandomBalanceAmount());

        //Act
        var balancesAreEqual = firstBalance == secondBalance;

        //Assert
        balancesAreEqual.Should().BeFalse();
    }

    private BalanceAmount GenerateRandomBalanceAmount()
    {
        return new BalanceAmount(Faker.Finance.Amount(min: -999999));
    }
}