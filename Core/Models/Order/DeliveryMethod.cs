﻿namespace Core.Models.Order
{
    public class DeliveryMethod : BaseProduct<int>
    {
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
    }
}
