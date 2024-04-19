using Core.Models.Basket;

namespace Core.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        public Task<Basket?> GetBasket(string id);
        public Task<Basket?> UpdateBasket(Basket ModifiedBasket);
        public Task<bool> DeleteBasket(string id);
    }
}
