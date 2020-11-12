using MoOnlineStore.Core.EntityClasses.OrderAggregate;

namespace MoOnlineStore.Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpefication<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string buyeremail):base(o => o.BuyerEmail  == buyeremail)
        {
            AddInclude(i => i.OrderItems);
            AddInclude(i => i.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification(string buyeremail,int orderid) 
            : base(o => o.ID == orderid && o.BuyerEmail == buyeremail)
        {
            AddInclude(i => i.OrderItems);
            AddInclude(i => i.DeliveryMethod);
        }
    }
}
