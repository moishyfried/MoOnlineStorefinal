using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Core.Interfaces
{
  public  interface IResponseCachedService
    {
        Task CacheResponseAsync(string CashID,object CacheResponse, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string CacheID);

    }
}
