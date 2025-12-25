using RabbitMQ.Client;

namespace FluxoDeCaixa.Infra.MessageBus.Configuration;

public class ConnectionRabbitMq
{
    private readonly string _connectionString;

    public ConnectionRabbitMq(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IConnection GetConnection()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_connectionString),
            ClientProvidedName = "FluxoDeCaixa"
        };
        return factory.CreateConnectionAsync().GetAwaiter().GetResult();
    }
}