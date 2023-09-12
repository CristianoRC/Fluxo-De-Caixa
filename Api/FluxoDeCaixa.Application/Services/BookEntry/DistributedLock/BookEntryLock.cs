using FluxoDeCaixa.Application.Exceptions;
using FluxoDeCaixa.Domain.Services.BookEntry;
using RedLockNet;

namespace FluxoDeCaixa.Application.Services.BookEntry.DistributedLock;

public class BookEntryLock : IBookEntryLock
{
    private readonly IDistributedLockFactory _distributedLockFactory;

    public BookEntryLock(IDistributedLockFactory distributedLockFactory)
    {
        _distributedLockFactory = distributedLockFactory;
    }
    
    public async Task Acquire(CreateBookEntry command)
    {
        var expiry = TimeSpan.FromSeconds(60);
        var wait = TimeSpan.FromSeconds(10);
        var retry = TimeSpan.FromSeconds(2);

        if (command.EntryBalance == command.OffsetBalance)
            throw new ArgumentException("Não foi possível obter o lock, balances são iguais");

        await using var lockEntry =
            await _distributedLockFactory.CreateLockAsync(command.EntryBalance.ToString(), expiry, wait, retry);
        if (lockEntry.IsAcquired is false)
            throw new LockNotAcquiredException("Não foi possível obter o lock do entry balance");

        await using var lockOffset =
            await _distributedLockFactory.CreateLockAsync(command.OffsetBalance.ToString(), expiry, wait, retry);
        if (lockOffset.IsAcquired is false)
        {
            await lockEntry.DisposeAsync();
            throw new LockNotAcquiredException("Não foi possível obter o lock do offset balance");
        }
    }
}