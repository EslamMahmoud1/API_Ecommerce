namespace Core.DataTransferObjects.Order
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public string BuyerEmail { get; set; }
        public int? DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; }

    }
}
