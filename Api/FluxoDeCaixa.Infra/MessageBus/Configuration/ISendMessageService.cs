namespace FluxoDeCaixa.Infra.MessageBus.Configuration;

public interface ISendMessageService
{
    public void Send(object message, string exchange, string routingKey);
    public void Send(object message, string queue);
}