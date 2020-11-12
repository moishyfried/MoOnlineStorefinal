using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoOnlineStore.Core.DTO
{
  public class BasketDto
    {
       
        [DataType(DataType.Password,ErrorMessage = "Please Enter A Valid Password")]
         [Required]
         public string ID { get; set; }
        public List<BasketItemDto> basketItems { get; set; } = new List<BasketItemDto>();
        public int? DeliveryMethodID { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntendID { get; set; }
        public decimal? ShippingPrice { get; set; } = 0;

    }
}
