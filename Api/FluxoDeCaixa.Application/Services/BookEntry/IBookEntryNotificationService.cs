namespace FluxoDeCaixa.Application.Services.BookEntry;

public interface IBookEntryNotificationService
{
    public void NewBookEntryCreates(Domain.Aggregations.BookEntry bookEntry, Guid commandCorrelationId);
}