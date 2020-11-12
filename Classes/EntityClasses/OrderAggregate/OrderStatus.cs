using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MoOnlineStore.Core.EntityClasses.OrderAggregate
{
  public  enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
         Pending,
        [EnumMember(Value ="Payment Recevied")]
        PaymentRecevied,
        [EnumMember(Value ="Payment Failed")]
        PaymentFailed
    }
}
