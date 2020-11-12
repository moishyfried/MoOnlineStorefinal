using MoOnlineStore.Core.EntityClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoOnlineStore.Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpefication<Products>
    {
        public ProductsWithFiltersForCountSpecification(ProductsSpecParams productsParams)
           : base(x => 
              (string.IsNullOrEmpty(productsParams.Search) || x.Name.ToLower()  
             .Contains(productsParams.Search)) &&
             (!productsParams.BrandId.HasValue || x.ProductBrandId == productsParams.BrandId) &&
             (!productsParams.TypeId.HasValue || x.ProductTypeId == productsParams.TypeId)
            )
        {
        }
    }
}
