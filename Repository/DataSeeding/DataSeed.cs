using Core.Models;
using Repository.Context;
using System.Text.Json;

namespace Repository.DataSeeding
{
    public static class DataSeed
    {
        public static async Task SeedData(ApiProjectContext context)
        {
            if (!context.Set<ProductBrand>().Any())
            {
                var brandsText = await File.ReadAllTextAsync(@"..\Repository\DataSeeding\brands.json");
                var brandsObject = JsonSerializer.Deserialize<List<ProductBrand>>(brandsText);

                if (brandsObject != null)
                {
                    await context.Set<ProductBrand>().AddRangeAsync(brandsObject);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<ProductType>().Any())
            {
                var typeText = await File.ReadAllTextAsync(@"..\Repository\DataSeeding\types.json");

                var typeObject = JsonSerializer.Deserialize<List<ProductType>>(typeText);

                if (typeObject != null)
                {
                    await context.Set<ProductType>().AddRangeAsync(typeObject);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Set<Product>().Any())
            {
                var ProductText = await File.ReadAllTextAsync(@"..\Repository\DataSeeding\products.json");
                var productObject = JsonSerializer.Deserialize<List<Product>>(ProductText);

                if (productObject != null)
                {
                    await context.Set<Product>().AddRangeAsync(productObject);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
