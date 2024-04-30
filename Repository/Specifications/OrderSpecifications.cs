using Core.Models.Order;

namespace Repository.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string userEmail)
        {
            Where = order => order.BuyerEmail == userEmail;

            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.orderItems);
        }

        public OrderSpecifications(Guid OrderId,string userEmail)
        {
            Where = order => order.BuyerEmail == userEmail && order.Id == OrderId;

            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.orderItems);
        }
    }
}
