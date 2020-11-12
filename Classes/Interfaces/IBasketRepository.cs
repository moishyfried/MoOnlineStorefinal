
using MoOnlineStore.Core.EntityClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasketEntity> GetBasketItems(string id);
        Task<CustomerBasketEntity> updateBasket(CustomerBasketEntity basket);
        Task<bool> deleteBasket(string id);
    }
}
