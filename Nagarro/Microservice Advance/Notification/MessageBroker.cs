using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification;

public class MessageBroker : BackgroundWorker
{
    private readonly IConnection _connection;
    private readonly IModel _model;
    private const string QueueName = "order-creation-queue";
    private const string ExchangeName = "order-exchange";


    public MessageBroker()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _model = _connection.CreateModel();
        _model.ExchangeDeclare(ExchangeName, "topic", false, false, null);
        _model.QueueDeclare(QueueName, false, false, false, null);
        _model.QueueBind(QueueName, ExchangeName, nameof(OrderStatus.Completed), null);
        _model.QueueBind(QueueName, ExchangeName, nameof(OrderStatus.Created), null);
        _model.QueueBind(QueueName, ExchangeName, nameof(OrderStatus.Failed), null);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_model);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderNotification = JsonConvert.DeserializeObject<OrderNotification>(message);

            Console.WriteLine($"Received order notification for Order ID: {orderNotification.OrderId}");
            Console.WriteLine($"Status: {orderNotification.Status}");
            Console.WriteLine("Products:");
            foreach (var product in orderNotification.Products)
            {
                // we can make the call to product details to fetch relevant information to display
                Console.WriteLine($"- Product ID: {product.ProductId}, Quantity: {product.Quantity}");
            }
        };

        _model.BasicConsume(queue: QueueName,
            autoAck: true,
            consumer: consumer);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        _model.Close();
    }
}