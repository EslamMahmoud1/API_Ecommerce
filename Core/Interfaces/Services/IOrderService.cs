using Core.DataTransferObjects.Order;
using Core.Models.Order;

namespace Core.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
        public Task<OrderResultDto> CreateOrderAsync(OrderDto input);
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id ,string email);
        public Task<IEnumerable<OrderResultDto>> GetOrdersAsync(string email);

    }
}
