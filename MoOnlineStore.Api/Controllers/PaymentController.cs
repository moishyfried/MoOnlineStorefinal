using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoOnlineStore.Api.Errors;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Infrastructure.Services;
using Stripe;
using Order = MoOnlineStore.Core.EntityClasses.OrderAggregate.Order;

namespace MoOnlineStore.Api.Controllers
{

    public class PaymentController : BaseApiController
    {
        private readonly IpaymentService _service;
        private readonly ILogger<PaymentService> _logger;
        private readonly string _stripeSecret;
        public PaymentController(IpaymentService service,ILogger<PaymentService> logger,IConfiguration config) 
        {
            _service = service;
            _logger = logger;
            _stripeSecret = config.GetSection("StripeSettings:stripeSecret").Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketEntity>> CreateOtUpdetPaymentIntend(string basketID)   
        {
            var basket = await _service.CreateOrUpdatePaymentIntent(basketID);
            if (basket == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
            return basket;
        }
        [HttpPost("webhooks")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _stripeSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("payment Succsseded", intent.Id);
                    order = await _service.UpdateOrderByPaymentSucseeded(intent.Id);
                    _logger.LogInformation("Order Updated", order.ID);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("payment failed", intent.Id);
                     order =  await _service.UpdateOrderByPaymentFailed(intent.Id);
                    _logger.LogInformation("Order Updated", order.ID);

                    break;
                default:
                    break;
            }
            return new EmptyResult();
        } 
    }
}