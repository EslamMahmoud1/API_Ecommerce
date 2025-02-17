﻿namespace Core.Models.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? PictureUrl { get; set; }
        public string? BrandName { get; set; }
        public string? TypeName { get; set; }

    }

}
