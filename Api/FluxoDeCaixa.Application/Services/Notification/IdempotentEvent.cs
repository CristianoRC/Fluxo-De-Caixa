namespace FluxoDeCaixa.Application.Services.Notification;

public class IdempotentEvent<T>
{
    public IdempotentEvent(Guid correlationId, T data)
    {
        IdempotencyKey = Guid.NewGuid();
        CorrelationId = correlationId;
        Data = data;
    }

    public Guid IdempotencyKey { get; }

    public Guid CorrelationId { get; }

    public T Data { get; }
}