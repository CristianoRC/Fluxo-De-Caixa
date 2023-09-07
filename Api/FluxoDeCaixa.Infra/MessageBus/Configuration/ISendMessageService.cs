namespace FluxoDeCaixa.Infra.MessageBus.Configuration;

public interface ISendMessageService
{
    public void SendMessage(object message, string exchange, string routingKey);
    public void SendMessage(object message, string queue);
}