namespace FluxoDeCaixa.Application.Services.BookEntry;

public interface IBookEntryNotificationService
{
    public void BookEntryCreated(Domain.Aggregations.BookEntry bookEntry, Guid commandCorrelationId);
}