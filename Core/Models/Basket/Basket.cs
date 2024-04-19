namespace Core.Models.Basket
{
    public class Basket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public int DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
