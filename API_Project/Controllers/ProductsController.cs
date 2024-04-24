using API_Project.Error;
using Core.DataTransferObjects;
using Core.Interfaces.Services;
using Core.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _Productservice;

        public ProductsController(IProductService service)
        {
            _Productservice = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters )
        {
            return Ok(await _Productservice.GetAllProductsAsync(parameters));
        }
        [HttpGet(template: "brands")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllBrands()
        {
            return Ok(await _Productservice.GetAllBrandsAsync());
        }
        [HttpGet(template: "types")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllTypes()
        {
            return Ok(await _Productservice.GetAllTypesAsync());
        }
        [HttpGet(template: "{id}")]
        [Authorize]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _Productservice.GetProductByIdAsync(id);
            return product is not null ? Ok(product) : NotFound(new ErrorResponseBody(404));
        }
    }
}
