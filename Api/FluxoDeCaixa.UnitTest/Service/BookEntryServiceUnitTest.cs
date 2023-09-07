using FluxoDeCaixa.Domain.Repositories;
using FluxoDeCaixa.Domain.Services.BookEntry;
using FluxoDeCaixa.UnitTest.Fakers;
using Moq;

namespace FluxoDeCaixa.UnitTest.Service;

public class BookEntryServiceUnitTest : BaseUnitTest
{
    [Fact(DisplayName = "Ao criar um book entry válido, deve salvar o resultado no banco")]
    public async Task ValidBookEntrySave()
    {
        //Arrange
        var createBookEntryCommand = new CreateBookEntry()
        {
            Amount = Faker.Finance.Amount(),
            EntryBalance = Guid.NewGuid(),
            OffsetBalance = Guid.NewGuid(),
            TransactionType = TransactionTypeFaker.GenerateRandomTransactionType()
        };

        var balanceRepository = new Mock<IBalanceRepository>();
        balanceRepository.Setup(x => x.Get(createBookEntryCommand.EntryBalance))
            .ReturnsAsync(BalanceFaker.GenerateValidBalance());
        balanceRepository.Setup(x => x.Get(createBookEntryCommand.OffsetBalance))
            .ReturnsAsync(BalanceFaker.GenerateValidBalance());

        var bookEntryRepository = new Mock<IBookEntryRepository>();
        var service = new BookEntryService(balanceRepository.Object, bookEntryRepository.Object);

        //Act
        var bookEntryCreated = await service.Create(createBookEntryCommand);

        //Assert
        bookEntryRepository.Verify(x => x.Save(bookEntryCreated), Times.Once);
    }

    [Fact(DisplayName = "Ao criar um book entry válido, deve retornar o resultado")]
    public async Task ValidBookEntryReturn()
    {
        //Arrange
        var createBookEntryCommand = new CreateBookEntry()
        {
            Amount = Faker.Finance.Amount(),
            EntryBalance = Guid.NewGuid(),
            OffsetBalance = Guid.NewGuid(),
            TransactionType = TransactionTypeFaker.GenerateRandomTransactionType()
        };
        var entryBalance = BalanceFaker.GenerateValidBalance();
        var offsetBalance = BalanceFaker.GenerateValidBalance();
        
        var balanceRepository = new Mock<IBalanceRepository>();
        balanceRepository.Setup(x => x.Get(createBookEntryCommand.EntryBalance))
            .ReturnsAsync(entryBalance);
        balanceRepository.Setup(x => x.Get(createBookEntryCommand.OffsetBalance))
            .ReturnsAsync(offsetBalance);

        var bookEntryRepository = new Mock<IBookEntryRepository>();
        var service = new BookEntryService(balanceRepository.Object, bookEntryRepository.Object);

        //Act
        var bookEntryCreated = await service.Create(createBookEntryCommand);

        //Assert
        bookEntryCreated.Errors.Should().BeEmpty();
        bookEntryCreated.Entry.Balance.Should().Be(entryBalance);
        bookEntryCreated.Entry.TransactionAmount.Value.Should().Be(createBookEntryCommand.Amount);
        bookEntryCreated.Offset.Balance.Should().Be(offsetBalance);
        bookEntryCreated.Offset.TransactionAmount.Value.Should().Be(createBookEntryCommand.Amount);
    }
}