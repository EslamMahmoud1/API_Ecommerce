using Core.DataTransferObjects;
using Core.Parameters;

namespace Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationsParameters specsParameters);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync();
        Task<ProductToReturnDto> GetProductByIdAsync(int id);
    }
}
