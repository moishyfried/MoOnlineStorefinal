using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.EntityClasses.OrderAggregate
{
    [Owned]
    public class ProductItemOrdered 
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemOrderedID, string productName, string pictureUrl)
        {
            ProductItemId = productItemOrderedID;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }


        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

    }
}
