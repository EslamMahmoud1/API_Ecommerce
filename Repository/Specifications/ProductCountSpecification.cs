using Core.Models;
using Core.Parameters;

namespace Repository.Specifications
{
    public class ProductCountSpecification : BaseSpecifications<Product>
    {
        public ProductCountSpecification(ProductSpecificationsParameters specParameters)
        {
            Where = product =>
            (!specParameters.TypeId.HasValue || product.Id == specParameters.TypeId.Value) &&
            (!specParameters.BrandId.HasValue || product.Id == specParameters.BrandId.Value) &&
            (String.IsNullOrWhiteSpace(specParameters.Search) || product.Name.ToLower().Contains(specParameters.Search));
        }
    }
}
