using AutoMapper;
using Core.DataTransferObjects;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Basket;

namespace Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _repository.DeleteBasket(id);
        }

        public async Task<BasketDto?> GetBasket(string id)
        {
            var basket = await _repository.GetBasket(id);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> UpdateBasket(BasketDto basket)
        {
            var mappedBasket = _mapper.Map<Basket>(basket);
            var updatedBasket = await _repository.UpdateBasket(mappedBasket);
            return updatedBasket is null ? null : _mapper.Map<BasketDto>(updatedBasket);
        }
    }
}
