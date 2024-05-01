using Core.DataTransferObjects.Order;
using Core.Interfaces.Services;
using Core.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderDto input)
        {
            var order = await _orderService.CreateOrderAsync(input);
            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersAsync(email);
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResultDto>> GetOrder(Guid orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdAsync(orderId,email);
            return Ok(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods() =>
            Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}
