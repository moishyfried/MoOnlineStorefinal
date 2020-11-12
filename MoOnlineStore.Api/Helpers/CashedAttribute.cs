using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MoOnlineStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Api.Helpers
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLive;

        public CashedAttribute(int timeToLive)
        {
            _timeToLive = timeToLive;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCachedService>();

            var cacheedKey = generateChashedKey(context.HttpContext.Request);

          var cacheResponse =  await cacheService.GetCacheResponseAsync(cacheedKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentresult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType ="application/json",
                    StatusCode = 200
                };
                context.Result = contentresult;
                return;
            }
            var exuitedResult = await next();

            if(exuitedResult.Result is OkObjectResult okObjectResult )
            {
                await cacheService.CacheResponseAsync(cacheedKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLive));
            }
            return;
        }

        private string generateChashedKey(HttpRequest request)
        {
            var sb = new StringBuilder();

            sb.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(q => q.Key))
            {
               sb.Append($"|{key}-{value}");
            };
            return sb.ToString();
            
        }
    }
}
