using AutoMapper;
using Core.DataTransferObjects;
using Core.Models;

namespace API_Project.Helper
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            return String.IsNullOrEmpty(source.PictureUrl) ? String.Empty : $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
