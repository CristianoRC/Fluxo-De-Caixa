using FluxoDeCaixa.Application.Exceptions;
using FluxoDeCaixa.Domain.Services.BookEntry;
using RedLockNet;

namespace FluxoDeCaixa.Application.Services.BookEntry;

public class BookEntryApplicationService : IBookEntryApplicationService
{
    private readonly IDistributedLockFactory _distributedLockFactory;
    private readonly IBookEntryService _bookEntryService;
    private readonly IBookEntryNotificationService _notificationService;

    public BookEntryApplicationService(IDistributedLockFactory distributedLockFactory, IBookEntryService bookEntryService, IBookEntryNotificationService notificationService)
    {
        _distributedLockFactory = distributedLockFactory;
        _bookEntryService = bookEntryService;
        _notificationService = notificationService;
    }

    public async Task<Domain.Aggregations.BookEntry> Create(CreateBookEntry command)
    {
        var expiry = TimeSpan.FromSeconds(60);
        var wait = TimeSpan.FromSeconds(10);
        var retry = TimeSpan.FromSeconds(2);

        if (command.EntryBalance == command.OffsetBalance)
            throw new ArgumentException("Não foi possível obter o lock, balances são iguais");
        
        await using var lockEntry = await _distributedLockFactory.CreateLockAsync(command.EntryBalance.ToString(), expiry, wait, retry);
        if (lockEntry.IsAcquired is false)
            throw new LockNotAcquiredException("Não foi possível obter o lock do entry balance");

        await using var lockOffset = await _distributedLockFactory.CreateLockAsync(command.OffsetBalance.ToString(), expiry, wait, retry);
        if (lockOffset.IsAcquired is false)
        {
            await lockEntry.DisposeAsync();
            throw new LockNotAcquiredException("Não foi possível obter o lock do offset balance");
        }

        var bookEntry = await _bookEntryService.Create(command);
        await lockEntry.DisposeAsync();
        await lockOffset.DisposeAsync();
        
        if (bookEntry.Errors.Any() is false)
            _notificationService.BookEntryCreated(bookEntry, command.CorrelationId);
        
        return bookEntry;
    }
}