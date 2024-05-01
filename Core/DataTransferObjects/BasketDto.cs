namespace Core.DataTransferObjects
{
    public class BasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int DeliveryMethod { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
