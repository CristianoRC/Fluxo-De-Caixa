using FluxoDeCaixa.Application.Services.BookEntry.DistributedLock;
using FluxoDeCaixa.Application.Services.Notification;
using FluxoDeCaixa.Domain.Services.BookEntry;

namespace FluxoDeCaixa.Application.Services.BookEntry;

public class BookEntryApplicationService : IBookEntryApplicationService
{
    private readonly IBookEntryLock _bookEntryLock;
    private readonly IBookEntryService _bookEntryService;
    private readonly IBookEntryNotificationService _notificationService;

    public BookEntryApplicationService(IBookEntryLock bookEntryLock, IBookEntryService bookEntryService,
        IBookEntryNotificationService notificationService)
    {
        _bookEntryLock = bookEntryLock;
        _bookEntryService = bookEntryService;
        _notificationService = notificationService;
    }

    public async Task<Domain.Aggregations.BookEntry> Create(CreateBookEntry command)
    {
        ArgumentNullException.ThrowIfNull(command);
        await _bookEntryLock.Acquire(command);
        var bookEntry = await _bookEntryService.Create(command);
        if (bookEntry.Errors.Any() is false)
            _notificationService.BookEntryCreated(bookEntry, command.CorrelationId);

        return bookEntry;
    }
}