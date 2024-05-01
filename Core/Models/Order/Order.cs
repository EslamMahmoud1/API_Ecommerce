namespace Core.Models.Order
{
    public class Order : BaseProduct<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingAddress shippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public IEnumerable<OrderItem> orderItems { get; set; }
        public PaymentStatus status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? BasketId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total => SubTotal + DeliveryMethod.Price;

    }
}
