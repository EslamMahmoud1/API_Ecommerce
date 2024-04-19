using Core.Interfaces.Repositories;
using Core.Models.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<Basket?> GetBasket(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket); 
        }

        public async Task<Basket?> UpdateBasket(Basket ModifiedBasket)
        {
            var SerielizedBasket = JsonSerializer.Serialize(ModifiedBasket);
            var flag = await _database.StringSetAsync(ModifiedBasket.Id, SerielizedBasket,TimeSpan.FromMinutes(3));
            return flag ? await GetBasket(ModifiedBasket.Id) : null;
        }
    }
}
