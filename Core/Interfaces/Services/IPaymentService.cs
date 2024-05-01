using Core.DataTransferObjects;
using Core.DataTransferObjects.Order;

namespace Core.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket);
        public Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId);
        public Task<OrderResultDto> UpdatePaymentStatusSucceeded(string paymentIntentId);
        public Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId);

    }
}
