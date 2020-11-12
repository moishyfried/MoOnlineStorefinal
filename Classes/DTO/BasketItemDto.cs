using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoOnlineStore.Core.DTO
{
    public class BasketItemDto
    {
        [Required]
        [DataType(DataType.Password)]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage ="Price Must Be Greater From One Dollar")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,double.MaxValue,ErrorMessage = "Quantity Must By Greater From One")]
        public int Quantity { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
