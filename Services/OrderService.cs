using AutoMapper;
using Core.DataTransferObjects.Order;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Order;
using Repository.Specifications;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResultDto> CreateOrderAsync(OrderDto order)
        {
            var basket = await _basketService.GetBasket(order.BasketId);
            if (basket == null) throw new Exception("basket error");
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if(product == null) continue;

                var orderItem = new OrderItem()
                {
                    Price = product.Price,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    ProductUrl = product.PictureUrl,
                    ProductId = product.Id,
                };
                orderItems.Add(orderItem);
            }

            if (order?.DeliveryMethodId == null) throw new Exception("invalid Delivery Method Id");

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(order.DeliveryMethodId.Value);

            if (deliveryMethod == null) throw new Exception("invalid Delivery Method");

            var shippingAddress = _mapper.Map<ShippingAddress>(order.Address);
            var createdOrder = new Order
            {
                BuyerEmail = order.BuyerEmail,
                orderItems = orderItems,
                DeliveryMethod = deliveryMethod,
                shippingAddress = shippingAddress,
                SubTotal = orderItems.Sum(item => item.Price * item.Quantity),
            };
            return _mapper.Map<OrderResultDto>(createdOrder);
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync() =>
            await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id, string email)
        {
            var spec = new OrderSpecifications(id, email);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationAsync(spec);
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersAsync(string email)
        {
            var spec = new OrderSpecifications(email);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetAllWithSpecificationsAsync(spec);
            return _mapper.Map<IEnumerable<OrderResultDto>>(order);
        }
    }
}
