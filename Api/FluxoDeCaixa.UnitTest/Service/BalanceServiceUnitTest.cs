using FluxoDeCaixa.Application.Services.Balance;
using FluxoDeCaixa.Domain.Entities;
using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Infra.Repositories;
using Moq;

namespace FluxoDeCaixa.UnitTest.Service;

public class BalanceServiceUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Nao deve salvar o balance caso ele seja inválido")]
    public async Task InvalidBalance()
    {
        //Arrange
        var repository = new Mock<IBalanceRepository>();
        var service = new BalanceApplicationService(repository.Object);

        //Act
        await service.Create(new CreateBalanceCommand());

        //Assert
        repository.Verify(x => x.Create(It.IsAny<Balance>()), Times.Never);
    }

    [Fact(DisplayName = "Deve retornar o balance caso ele seja inválido")]
    public async Task InvalidBalanceResponse()
    {
        //Arrange
        var repository = new Mock<IBalanceRepository>();
        var service = new BalanceApplicationService(repository.Object);

        //Act
        var balance = await service.Create(new CreateBalanceCommand());

        //Assert
        balance.IsValid.Should().BeFalse();
        balance.Name.Should().BeNull();
    }

    [Fact(DisplayName = "Deve salvar caso o balance seja válido")]
    public async Task SaveBalance()
    {
        //Arrange
        var repository = new Mock<IBalanceRepository>();
        var service = new BalanceApplicationService(repository.Object);

        var createBalanceCommand = new CreateBalanceCommand()
        {
            Name = Faker.Lorem.Word()
        };

        //Act
        var createdBalance = await service.Create(createBalanceCommand);

        //Assert
        repository.Verify(x =>
            x.Create(It.Is<Balance>(b => b.Name == createBalanceCommand.Name)), Times.Once);
        createdBalance.Name.Should().Be(createBalanceCommand.Name);
    }
}