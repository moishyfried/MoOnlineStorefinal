using MoOnlineStore.Core.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoOnlineStore.Core.DTO
{
    public class DTOProducts :DTOBaseEntity
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
    }
}
