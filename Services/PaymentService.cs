using AutoMapper;
using Core.DataTransferObjects;
using Core.DataTransferObjects.Order;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Order;
using Microsoft.Extensions.Configuration;
using Repository.Specifications;
using Stripe;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PaymentService(IUnitOfWork unitOfWork, IBasketService basketService, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForExistingOrder(BasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SeceretKey"];

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Core.Models.Product, int>().GetByIdAsync(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;
            }

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethod);
            var price = basket.Items.Sum(i => i.Price * i.Quantity);
            long amount = (long)(price * 100 + deliveryMethod.Price * 100);

            var paymentService = new PaymentIntentService();
            if (String.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await paymentService.CreateAsync(options);
                basket.ClientSecret = paymentIntent.ClientSecret;
                basket.PaymentIntentId = paymentIntent.Id;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await paymentService.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketService.UpdateBasket(basket);
            return basket;
        }

        public async Task<BasketDto> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SeceretKey"];

            var basket = await _basketService.GetBasket(basketId);

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Core.Models.Product, int>().GetByIdAsync(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;
            }

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethod);
            var price = basket.Items.Sum(i => i.Price * i.Quantity);
            long amount = (long)(price * 100 + deliveryMethod.Price * 100);

            var paymentService = new PaymentIntentService();
            if (String.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await paymentService.CreateAsync(options);
                basket.ClientSecret = paymentIntent.ClientSecret;
                basket.PaymentIntentId = paymentIntent.Id;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await paymentService.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketService.UpdateBasket(basket);
            return basket;
        }

        public async Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var spec = new PaymentIntentForOrderSpecification(paymentIntentId);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationAsync(spec);
            if (order == null) throw new Exception("Wrong PaymentIntent Id");
            order.status = PaymentStatus.Failed;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<OrderResultDto> UpdatePaymentStatusSucceeded(string paymentIntentId)
        {
            var spec = new PaymentIntentForOrderSpecification(paymentIntentId);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationAsync(spec);
            if (order == null) throw new Exception("Wrong PaymentIntent Id");
            order.status = PaymentStatus.Failed;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            await _basketService.DeleteBasket(order.BasketId!);
            return _mapper.Map<OrderResultDto>(order);
        }
    }
}
