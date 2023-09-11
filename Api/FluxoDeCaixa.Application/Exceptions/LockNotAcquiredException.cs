namespace FluxoDeCaixa.Application.Exceptions;

public class LockNotAcquiredException : Exception
{
    public LockNotAcquiredException(string message) : base(message)
    {
    }
}