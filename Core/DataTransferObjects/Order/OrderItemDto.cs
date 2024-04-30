﻿namespace Core.DataTransferObjects.Order
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}
