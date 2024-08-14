using CheckoutService.Enums;
using System.Text.Json;
using RabbitMQ.Client;
namespace CheckoutService.ServiceImplementations;

public class MessageServiceAction:IMessageService
{
    private readonly IModel _model;

    public MessageServiceAction()
    {
        var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST")??"localhost",
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME")??"guest",
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")??"guest"
        };

         var connection = factory.CreateConnection();
        _model = connection.CreateModel();
        _model.ExchangeDeclare("order-exchange", "topic", false, false, null);
    }
    public void PublishMessage(OrderStatus orderStatus, object message)
    {
        try
        {
            var json = JsonSerializer.Serialize(message);
            var body = System.Text.Encoding.UTF8.GetBytes(json);
            var basicProperties = _model.CreateBasicProperties();
            basicProperties.ContentType = "application/json";
            _model.BasicPublish(
                "order-exchange",
                nameof(orderStatus), 
                basicProperties, 
                body);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}