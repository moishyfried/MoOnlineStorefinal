using MoOnlineStore.Core.DTO.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.DTO
{
   public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
