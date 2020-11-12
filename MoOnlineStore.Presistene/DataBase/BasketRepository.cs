using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.DataBase
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> deleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasketEntity> GetBasketItems(string id)
        {
            var data = await _database.StringGetAsync(id);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasketEntity>(data);
        }

        public async Task<CustomerBasketEntity> updateBasket(CustomerBasketEntity basket)
        {
            var created = await _database.StringSetAsync(basket.ID, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));

            if (!created) return null;

            var newbasket = await GetBasketItems(basket.ID);
            return newbasket;   
        }
    }
}
