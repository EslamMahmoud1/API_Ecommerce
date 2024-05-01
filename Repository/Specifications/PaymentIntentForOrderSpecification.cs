using Core.Models.Order;

namespace Repository.Specifications
{
    public class PaymentIntentForOrderSpecification : BaseSpecifications<Order>
    {
        public PaymentIntentForOrderSpecification(string paymentIntentId)
        {
            Where = order => order.PaymentIntentId == paymentIntentId;
        }
    }
}
