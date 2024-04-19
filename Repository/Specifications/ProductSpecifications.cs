using Core.Models;
using Core.Parameters;

namespace Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications(ProductSpecificationsParameters specs) 
        {
            Where = product =>
            (!specs.TypeId.HasValue || product.Id == specs.TypeId.Value) &&
            (!specs.BrandId.HasValue || product.Id == specs.BrandId.Value) &&
            (String.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search));

            Includes.Add(product => product.productType);
            Includes.Add(product => product.productBrand);

            ApplyPagination(specs.PageIndex, specs.PageSize);

            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case ProductSortingValues.NameAsc:
                        OrderBy = product => product.Name;
                        break;
                    case ProductSortingValues.NameDesc:
                        OrderByDesc = product => product.Name;
                        break;
                    case ProductSortingValues.PriceAsc:
                        OrderBy = product => product.Price;
                        break;
                    case ProductSortingValues.PriceDesc:
                        OrderByDesc = product => product.Price;
                        break;
                    default:
                        OrderBy = product => product.Name;
                        break;
                }
            }
        }
        public ProductSpecifications(int id) 
        {
            Where = product =>product.Id == id;

            Includes.Add(product => product.productType);
            Includes.Add(product => product.productBrand);
        }
    }
}
