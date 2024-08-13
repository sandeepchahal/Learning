using CheckoutService.Enums;

namespace CheckoutService.ServiceImplementations;

public interface IMessageService
{
    void PublishMessage(OrderStatus orderStatus, object message);
}