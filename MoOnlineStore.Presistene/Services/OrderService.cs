using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.Services
{
      public class OrderService : IOrderService
      {
            private readonly IBasketRepository _basketRepository;
            private readonly IUnitOfWork _unitOfWork;
        private readonly IpaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork ,IpaymentService paymentService)
            {
                  _basketRepository = basketRepository;
                  _unitOfWork = unitOfWork;
                   _paymentService = paymentService;
        }

            public async Task<Order> CreateOrderAsync(string buyerEmail, int DeliveryOptionId, string basketID, Address shipingAddress)
            {
                  //getting the basket
                  var basket = await _basketRepository.GetBasketItems(basketID);
                  if(basket == null ||basket.basketItems == null || !basket.DeliveryMethodID.HasValue) {return null;}
                  var orderItems = new List<OrderItem>();
                  //getting the items from the basket  
                  
                  foreach (var item in basket.basketItems)
                  {
                        var product = await _unitOfWork.repositorys<Products>().GetByID(item.Id);
                        var itemordered = new ProductItemOrdered(product.ID, product.Name, product.PictureUrl);
                        var orderitem = new OrderItem(itemordered, item.Quantity, product.Price);
                        orderItems.Add(orderitem);
                  }

                  //getting the delivey option
                  var deliveryOption = await _unitOfWork.repositorys<DeliveryMethod>().GetByID(DeliveryOptionId);

                  //colclating the cust
                  var subtotal = orderItems.Sum(s => s.Price * s.Quantity + deliveryOption.Price);
            //creating the order

            var spec = new OrderByPaymentIntendSpecification(basket.PaymentIntendID);
            var previosOrder = await _unitOfWork.repositorys<Order>().GetEntityWithSpec(spec);
            if(previosOrder != null)
            {
                _unitOfWork.repositorys<Order>().Delete(previosOrder);
                 await  _paymentService.CreateOrUpdatePaymentIntent(basket.ID);
            }
            
            var order = new Order(orderItems, buyerEmail, shipingAddress, subtotal, deliveryOption,basket.PaymentIntendID);
              _unitOfWork.repositorys<Order>().Add(order);
                  //saving to db
                  var result = await _unitOfWork.complete();
                  if (result <= 0) return null;
                  
                  return order;
            }

            public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            {
                  IReadOnlyList<DeliveryMethod> deliveymethods = await _unitOfWork.repositorys<DeliveryMethod>().ListAllAsync();
                  return deliveymethods;
            }

            public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
            {
                  var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail, id);
                  return await _unitOfWork.repositorys<Order>().GetEntityWithSpec(spec);
            }

            public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
            {
                  var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
                  return await _unitOfWork.repositorys<Order>().ListWithSpecAsync(spec);
            }
      }
}
