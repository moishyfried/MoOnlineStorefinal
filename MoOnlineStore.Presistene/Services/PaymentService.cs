using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Core.Specifications;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = MoOnlineStore.Core.EntityClasses.OrderAggregate.Order;

namespace MoOnlineStore.Infrastructure.Services
{
    public class PaymentService : IpaymentService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IConfiguration config ,IUnitOfWork unitOfWork ,IBasketRepository basketRepository)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasketEntity> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var basket = await _basketRepository.GetBasketItems(basketId);

            if (basket == null || basket.basketItems == null || !basket.DeliveryMethodID.HasValue) { return null; }

            var shippingPrice = 0m;
            if (basket.DeliveryMethodID.HasValue)
            {
                var deliveryMethod = await _unitOfWork.repositorys<DeliveryMethod>()
                    .GetByID((int)basket.DeliveryMethodID);
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var item in basket.basketItems)
            {
                var product = await _unitOfWork.repositorys<Products>().GetByID(item.Id);
                if(item.Price != product.Price)
                {
                    item.Price = product.Price;
                }
            }
           
            var service = new PaymentIntentService();

            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntendID))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.basketItems.Sum(i => i.Quantity * (i.Price * 100))
                    + (long)shippingPrice,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }

                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntendID = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }

            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.basketItems.Sum(i => i.Quantity * (i.Price * 100))
                    + (long)shippingPrice
                };
                await service.UpdateAsync(basket.PaymentIntendID, options);
            }
            await _basketRepository.updateBasket(basket);
            return basket;
        }

        public async Task<Order> UpdateOrderByPaymentFailed(string paymentIntendID)
        {
            var spec = new OrderByPaymentIntendSpecification(paymentIntendID);

            var order = await _unitOfWork.repositorys<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;

            order.Status = OrderStatus.PaymentFailed;

             _unitOfWork.repositorys<Order>().Update(order);

             await  _unitOfWork.complete();

            return order;
        }

        public async Task<Order> UpdateOrderByPaymentSucseeded(string paymentIntendID)
        {
            var spec = new OrderByPaymentIntendSpecification(paymentIntendID);

            var order = await _unitOfWork.repositorys<Order>().GetEntityWithSpec(spec);
            if (order == null) return null;

            order.Status = OrderStatus.PaymentRecevied;

            _unitOfWork.repositorys<Order>().Update(order);

            await _unitOfWork.complete();

            return order;
        }

        
    }
}
