using AutoMapper;
using Microsoft.Extensions.Configuration;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;

namespace MoOnlineStore.Core.Helpers
{
    public class ProductUrlResolver:IValueResolver<Products,DTOProducts,string> 
    {  
        public IConfiguration _Config { get; }

        public ProductUrlResolver(IConfiguration config)
        {
            _Config = config;
        }

        public string Resolve(Products source, DTOProducts destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _Config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}