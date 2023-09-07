using FluxoDeCaixa.Domain.Entities;

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
}