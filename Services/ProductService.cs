using AutoMapper;
using Core.DataTransferObjects;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Parameters;
using Repository.Specifications;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandTypeDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(Brands);
        }

        public async Task<PaginatedResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationsParameters specsParameters)
        {
            var specs = new ProductSpecifications(specsParameters);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(specs);
            var countSpec = new ProductCountSpecification(specsParameters);
            var count = (await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(countSpec)).Count();
            var mappedProduct = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(Products);
            return new PaginatedResultDto<ProductToReturnDto>
            {
                PageIndex = specsParameters.PageIndex,
                PageSize = specsParameters.PageSize,
                TotalCount = count,
                Data = mappedProduct,
            };
        }

        public async Task<IEnumerable<BrandTypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandTypeDto>>(Types);
        }

        public async Task<ProductToReturnDto> GetProductByIdAsync(int id)
        {
            var specs = new ProductSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationAsync(specs);
            return _mapper.Map<ProductToReturnDto>(Product);
        }
    }
}
