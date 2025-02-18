using RabbitMQ.Client;
using System.Text;

public class RabbitMQService
{
    private readonly string _hostName = "localhost"; 
    private readonly string _queueName = "fila_formulario";
    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = _hostName };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

       
        channel.QueueDeclare(queue: _queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: _queueName,
                             basicProperties: null,
                             body: body);
    }
}
