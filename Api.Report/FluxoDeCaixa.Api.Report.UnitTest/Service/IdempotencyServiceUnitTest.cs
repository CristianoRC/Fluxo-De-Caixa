using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Domain.Service;
using FluxoDeCaixa.Api.Report.UnitTest.Faker;
using Moq;

namespace FluxoDeCaixa.Api.Report.UnitTest.Service;

public class IdempotencyServiceUnitTest
{
    [Fact(DisplayName = "Deve retornar como já processado se achave for nula")]
    public async Task NullIdempotencyKey()
    {
        //Arrange
        var repository = new Mock<IIdempotencyRepository>();
        var service = new IdempotencyService(repository.Object);
        FakeIdempotencyKey idempotencyKey = null;

        //Act
        var messageAlreadyProcess = await service.MessageAlreadyProcessed(idempotencyKey!);

        //Assert
        messageAlreadyProcess.Should().BeTrue();
        repository.Verify(x => x.AlreadyProcess(It.IsAny<Guid>()), Times.Never);
    }

    [Fact(DisplayName = "Deve retornar como já processado se achave tiver o valor padrão")]
    public async Task EmptyIdempotencyKey()
    {
        //Arrange
        var repository = new Mock<IIdempotencyRepository>();
        var service = new IdempotencyService(repository.Object);
        var idempotencyKey = new FakeIdempotencyKey();

        //Act
        var messageAlreadyProcess = await service.MessageAlreadyProcessed(idempotencyKey);

        //Assert
        messageAlreadyProcess.Should().BeTrue();
        repository.Verify(x => x.AlreadyProcess(It.IsAny<Guid>()), Times.Never);
    }

    [Theory(DisplayName = "Se a chave form válida, deve retornar o resultado do repository")]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ValidKey(bool alreadyProcess)
    {
        //Arrange
        var idempotency = new FakeIdempotencyKey {IdempotencyKey = Guid.NewGuid()};
        var repository = new Mock<IIdempotencyRepository>();
        repository.Setup(x => x.AlreadyProcess(idempotency.IdempotencyKey)).ReturnsAsync(alreadyProcess);
        var service = new IdempotencyService(repository.Object);

        //Act
        var messageAlreadyProcess = await service.MessageAlreadyProcessed(idempotency);

        //Assert
        messageAlreadyProcess.Should().Be(alreadyProcess);
        repository.Verify(x => x.AlreadyProcess(idempotency.IdempotencyKey), Times.Once);
    }

    [Fact(DisplayName = "Não deve marcar como processada caso a chave seja null")]
    public async Task NullKeyOnSave()
    {
        //Arrange
        var repository = new Mock<IIdempotencyRepository>();
        var service = new IdempotencyService(repository.Object);
        FakeIdempotencyKey idempotencyKey = null;

        //Act
        await service.MarkAsProcessed(idempotencyKey!);

        //Assert
        repository.Verify(x => x.MarkAsProcess(It.IsAny<Guid>()), Times.Never);
    }

    [Fact(DisplayName = "Não deve marcar como processada caso a chave tenha o valor padrão")]
    public async Task EmptyKeyOnSave()
    {
        //Arrange
        var repository = new Mock<IIdempotencyRepository>();
        var service = new IdempotencyService(repository.Object);
        var idempotencyKey = new FakeIdempotencyKey();

        //Act
        await service.MarkAsProcessed(idempotencyKey!);

        //Assert
        repository.Verify(x => x.MarkAsProcess(It.IsAny<Guid>()), Times.Never);
    }

    [Fact(DisplayName = "Deve marcar como processada caso a chave seja válida")]
    public async Task MaskAsProcessValidKey()
    {
        //Arrange
        var repository = new Mock<IIdempotencyRepository>();
        var service = new IdempotencyService(repository.Object);
        var idempotencyKey = new FakeIdempotencyKey() {IdempotencyKey = Guid.NewGuid()};

        //Act
        await service.MarkAsProcessed(idempotencyKey!);

        //Assert
        repository.Verify(x => x.MarkAsProcess(idempotencyKey.IdempotencyKey), Times.Once());
    }
}