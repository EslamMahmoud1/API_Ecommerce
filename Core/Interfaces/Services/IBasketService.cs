using Core.DataTransferObjects;

namespace Core.Interfaces.Services
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasket(string id);
        Task<BasketDto?> UpdateBasket(BasketDto basket);
        Task<bool> DeleteBasket(string id);
    }
}
