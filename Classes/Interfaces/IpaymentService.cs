using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
  public  interface IpaymentService
    {
        Task<CustomerBasketEntity> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderByPaymentSucseeded(string paymentIntendID);
        Task<Order> UpdateOrderByPaymentFailed(string paymentIntendID);

    }
}
