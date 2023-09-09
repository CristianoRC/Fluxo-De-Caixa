namespace FluxoDeCaixa.Api.Report.Domain.Service.BookEntry;

public interface IBookEntryService
{
    Task Create(Entities.BookEntry createBookEntry);
}