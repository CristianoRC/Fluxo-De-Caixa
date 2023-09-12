using FluxoDeCaixa.Application.Services.BookEntry;
using FluxoDeCaixa.Application.Services.BookEntry.DistributedLock;
using FluxoDeCaixa.Application.Services.Notification;
using FluxoDeCaixa.Domain.Aggregations;
using FluxoDeCaixa.Domain.Services.BookEntry;
using FluxoDeCaixa.Domain.ValueObjects;
using FluxoDeCaixa.UnitTest.Fakers;
using Moq;

namespace FluxoDeCaixa.UnitTest.Application.Service;

public class BookEntryApplicationServiceUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Deve dar erro ArgumentNullException caso o command seja null")]
    public async Task NullCommand()
    {
        //Arrange
        var bookEntryLock = new Mock<IBookEntryLock>();
        var bookEntryService = new Mock<IBookEntryService>();
        var notificationService = new Mock<IBookEntryNotificationService>();
        var service =
            new BookEntryApplicationService(bookEntryLock.Object, bookEntryService.Object, notificationService.Object);

        //Act
        var onCreateBookEntry = async () => await service.Create(null!);

        //Assert
        await onCreateBookEntry.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact(DisplayName = "Se o command for válido deve tentar obter o lock")]
    public async Task LockTest()
    {
        //Arrange
        var command = GenerateValidCommand();
        var bookEntry = BookEntryFaker.GenerateValidBookEntry();
        var bookEntryLock = new Mock<IBookEntryLock>();
        var bookEntryService = new Mock<IBookEntryService>();
        bookEntryService.Setup(x => x.Create(command))
            .ReturnsAsync(bookEntry);
        var notificationService = new Mock<IBookEntryNotificationService>();
        var service =
            new BookEntryApplicationService(bookEntryLock.Object, bookEntryService.Object, notificationService.Object);

        //Act
        await service.Create(command);

        //Assert
        bookEntryLock.Verify(x => x.Acquire(command), Times.Once);
    }

    [Fact(DisplayName = "Deve notificar caso o Book entry não tenha erros")]
    public async Task ShouldNotify()
    {
        //Arrange
        var command = GenerateValidCommand();
        var bookEntry = BookEntryFaker.GenerateValidBookEntry();
        var bookEntryLock = new Mock<IBookEntryLock>();
        var bookEntryService = new Mock<IBookEntryService>();
        bookEntryService.Setup(x => x.Create(command))
            .ReturnsAsync(bookEntry);
        var notificationService = new Mock<IBookEntryNotificationService>();
        var service =
            new BookEntryApplicationService(bookEntryLock.Object, bookEntryService.Object, notificationService.Object);

        //Act
        await service.Create(command);

        //Assert
        notificationService.Verify(x => x.BookEntryCreated(bookEntry, command.CorrelationId));
    }

    [Fact(DisplayName = "Não deve notificar caso o Book entry tenha erros")]
    public async Task ShouldNotNotify()
    {
        //Arrange
        var command = GenerateValidCommand();
        var bookEntry = BookEntryFaker.GenerateInvalidBookEntry();
        var bookEntryLock = new Mock<IBookEntryLock>();
        var bookEntryService = new Mock<IBookEntryService>();
        bookEntryService.Setup(x => x.Create(command))
            .ReturnsAsync(bookEntry);
        var notificationService = new Mock<IBookEntryNotificationService>();
        var service =
            new BookEntryApplicationService(bookEntryLock.Object, bookEntryService.Object, notificationService.Object);

        //Act
        await service.Create(command);

        //Assert
        notificationService.Verify(x => x.BookEntryCreated(It.IsAny<BookEntry>(), It.IsAny<Guid>()), Times.Never);
    }

    private CreateBookEntry GenerateValidCommand()
    {
        return new CreateBookEntry()
        {
            Amount = Faker.Finance.Amount(),
            Description = Faker.Lorem.Word(),
            EntryBalance = Guid.NewGuid(),
            OffsetBalance = Guid.NewGuid(),
            TransactionType = TransactionType.Credit,
            CorrelationId = Guid.NewGuid()
        };
    }
}