
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.EntityClasses
{
  public  class CustomerBasketEntity
    {
        public CustomerBasketEntity()
        {
        }

        public CustomerBasketEntity(string iD)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
        }

        public string ID { get; set; }

        public List<BasketItemsEntity> basketItems { get; set; } = new List<BasketItemsEntity>();

        public int? DeliveryMethodID { get; set; }
        public string  ClientSecret { get; set; }
        public string PaymentIntendID { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
