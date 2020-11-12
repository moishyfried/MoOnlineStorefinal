using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Interfaces;

namespace MoOnlineStore.Api.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasketEntity>>GetBasket(string id)
        {
            var data = await _basketRepo.GetBasketItems(id);

            return Ok(data ?? new CustomerBasketEntity(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketEntity>> UpdateBasket(BasketDto basket)
        {
              var entityBasket =  _mapper.Map<BasketDto, CustomerBasketEntity>(basket);
              var items =  await _basketRepo.updateBasket(entityBasket);
              return Ok(items ?? new CustomerBasketEntity(basket.ID));
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepo.deleteBasket(id);
        }
    }
}