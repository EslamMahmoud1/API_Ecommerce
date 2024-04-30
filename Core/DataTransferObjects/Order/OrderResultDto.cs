using Core.Models.Order;

namespace Core.DataTransferObjects.Order
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDto shippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IEnumerable<OrderItemDto> orderItems { get; set; }
        public PaymentStatus status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total {  get; set; }
    }
}
