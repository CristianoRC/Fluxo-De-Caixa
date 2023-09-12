using FluxoDeCaixa.Infra.MessageBus.Configuration;

namespace FluxoDeCaixa.Application.Services.Notification;

public class BookEntryNotificationService : IBookEntryNotificationService
{
    private readonly ISendMessageService _sendMessageService;

    public BookEntryNotificationService(ISendMessageService sendMessageService)
    {
        _sendMessageService = sendMessageService;
    }

    public void BookEntryCreated(Domain.Aggregations.BookEntry bookEntry, Guid commandCorrelationId)
    {
        var message = new IdempotentEvent<Domain.Aggregations.BookEntry>(commandCorrelationId, bookEntry);
        _sendMessageService.Send(message, "book-entry", string.Empty);
    }
}