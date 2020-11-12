using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MoOnlineStore.Core.Specifications
{
    public class OrderByPaymentIntendSpecification : BaseSpefication<Order>
    {
        public OrderByPaymentIntendSpecification(string paymentIndendID)
            : base(o => o.PaymentIntentId == paymentIndendID)
        {
        }
    }
}
