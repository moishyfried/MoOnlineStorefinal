using MoOnlineStore.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure.Services
{
    public class ResponseCachedService : IResponseCachedService
    {
        private IDatabase _database;
        public ResponseCachedService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string CashID, object CacheResponse, TimeSpan timeToLive)
        {
            if(CacheResponse == null)
            {
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serilizer = JsonSerializer.Serialize(CacheResponse, options);
             await  _database.StringSetAsync(CashID,serilizer, timeToLive);
           }

        public async Task<string> GetCacheResponseAsync(string CacheID)
        {
            var response = await _database.StringGetAsync(CacheID);
            if (response.IsNullOrEmpty) return null;

            return response;

        }
    }
}
