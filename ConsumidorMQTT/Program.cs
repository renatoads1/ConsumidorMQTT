
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost"
};

using (var connection = factory.CreateConnection())

using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
            queue: "msg_01",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

    var _consumer = new EventingBasicConsumer(channel);

        _consumer.Received += (model, ea) =>
        {
        var body = ea.Body.ToArray();
        var msg = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[x] Recebida:{msg}");
        };
        channel.BasicConsume(
                queue: "msg_01",
                autoAck: true,
                consumer: _consumer
            );
        Console.WriteLine("consumida fila inteira");
        Thread.Sleep(2000);
    

}
Console.ReadKey();