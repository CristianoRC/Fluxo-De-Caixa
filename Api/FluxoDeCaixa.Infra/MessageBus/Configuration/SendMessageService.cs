using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FluxoDeCaixa.Infra.MessageBus.Configuration;

public class SendMessageService : ISendMessageService
{
    private readonly IConnection _rabbitMqConnection;

    public SendMessageService(IConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public void Send(object message, string exchange, string routingKey)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        using var channel = _rabbitMqConnection.CreateModel();
        channel.BasicPublish(exchange, routingKey, null, body);
    }

    public void Send(object message, string queue)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        using var channel = _rabbitMqConnection.CreateModel();

        channel.BasicPublish(string.Empty, queue, null, body);
    }
}