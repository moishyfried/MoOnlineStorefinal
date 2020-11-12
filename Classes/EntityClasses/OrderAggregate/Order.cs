using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.EntityClasses.OrderAggregate
{
   public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItem, string buyerEmail, Address eliveryAddress, decimal subtotal, DeliveryMethod deliveryMethod,string paymentIntendID)
        {
            OrderItems = orderItem;
            BuyerEmail = buyerEmail ;
            this.ShipToAddress = eliveryAddress ;
            Subtotal = subtotal;
            this.DeliveryMethod = deliveryMethod ;
            this.PaymentIntentId = paymentIntendID;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
