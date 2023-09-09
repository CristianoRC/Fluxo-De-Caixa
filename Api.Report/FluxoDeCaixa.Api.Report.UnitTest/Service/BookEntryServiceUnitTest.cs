using FluxoDeCaixa.Api.Report.Domain.Entities;
using FluxoDeCaixa.Api.Report.Domain.Repository;
using FluxoDeCaixa.Api.Report.Domain.Service;
using FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;
using FluxoDeCaixa.Api.Report.UnitTest.Faker;
using Moq;

namespace FluxoDeCaixa.Api.Report.UnitTest.Service;

public class BookEntryServiceUnitTest
{
    [Fact(DisplayName = "Não deve processar se o book entry já tiver sido processado")]
    public async Task AlreadyProcessed()
    {
        //Arrange
        var bookEntry = BookEntryFaker.GenerateValidBookEntry();
        var idempotencyService = new Mock<IIdempotencyService>();
        idempotencyService.Setup(x => x.MessageAlreadyProcessed(bookEntry))
            .ReturnsAsync(true);
        var bookEntryRepository = new Mock<IBookEntryRepository>();
        var bookEntryService = new BookEntryService(idempotencyService.Object, bookEntryRepository.Object);

        //Act
        await bookEntryService.Create(bookEntry);

        //Assert
        bookEntryRepository.Verify(x => x.SaveTransaction(It.IsAny<IEnumerable<Transaction>>()), Times.Never);
    }
}