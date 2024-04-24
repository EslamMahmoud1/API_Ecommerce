using API_Project.Error;
using Core.DataTransferObjects;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basket;

        public BasketController(IBasketService basket)
        {
            _basket = basket;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _basket.GetBasket(id);
            return basket is null ? NotFound(new ErrorResponseBody(404,"basket not found")) : Ok(basket);    
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _basket.UpdateBasket(basketDto);
            return basket is null ? BadRequest(new ErrorResponseBody(401, "Bad Request")) : Ok(basket); 
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            return Ok(await _basket.DeleteBasket(id));
        }
    }
}
