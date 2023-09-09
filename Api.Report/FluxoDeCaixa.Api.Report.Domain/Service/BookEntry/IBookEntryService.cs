using FluxoDeCaixa.Api.Report.Domain.Entities;

namespace FluxoDeCaixa.Api.Report.Domain.Service;

public interface IBookEntryService
{
    Task Create(BookEntry createBookEntry);
}