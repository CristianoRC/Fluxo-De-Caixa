using FluxoDeCaixa.Application.Services.BookEntry;
using FluxoDeCaixa.Infra.MessageBus.Configuration;

namespace FluxoDeCaixa.Application.Services.Notification;

public class BookEntryNotificationService : IBookEntryNotificationService
{
    private readonly ISendMessageService _sendMessageService;

    public BookEntryNotificationService(ISendMessageService sendMessageService)
    {
        _sendMessageService = sendMessageService;
    }

    public void NewBookEntryCreates(Domain.Aggregations.BookEntry bookEntry, Guid commandCorrelationId)
    {
        var message = new IdempotentEvent<Domain.Aggregations.BookEntry>(commandCorrelationId, bookEntry);
        _sendMessageService.SendMessage(message, "book-entry", string.Empty);
    }
}