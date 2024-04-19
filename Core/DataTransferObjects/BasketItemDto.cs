using System.ComponentModel.DataAnnotations;

namespace Core.DataTransferObjects
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 99)]
        public int Quantity { get; set; }
        public string? PictureUrl { get; set; }
        public string? BrandName { get; set; }
        public string? TypeName { get; set; }
    }
}
