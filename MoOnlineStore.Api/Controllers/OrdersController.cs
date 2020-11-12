using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoOnlineStore.Api.Errors;
using MoOnlineStore.Api.Extenshions;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.DTO.identity;
using MoOnlineStore.Core.EntityClasses.OrderAggregate;
using MoOnlineStore.Core.Interfaces;

namespace MoOnlineStore.Api.Controllers
{
  [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;   
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
             
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> listorders()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrderForUserAsync(email);

            if (orders == null) return BadRequest(new ApiResponse(404, "Error By Getting The Orders"));

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> gaveOrderByID(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id,email);

            if (order == null) return NotFound(new ApiResponse(404, "Error By Getting The Orders"));
               var dtoOrder =   _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(dtoOrder);
        }
        [HttpGet("deliverymethods")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliveyMethods = await _orderService.GetDeliveryMethodsAsync();
            if (deliveyMethods == null) return NotFound(new ApiResponse(404, "Error By Getting The Deliver Options"));

            return Ok(deliveyMethods);
        }
    }
}