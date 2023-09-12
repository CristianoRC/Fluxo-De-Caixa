namespace FluxoDeCaixa.Application.Services.Notification;

public interface IBookEntryNotificationService
{
    public void BookEntryCreated(Domain.Aggregations.BookEntry bookEntry, Guid commandCorrelationId);
}