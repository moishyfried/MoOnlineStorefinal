using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Specifications;

namespace MoOnlineStore.Core.specifications
{
    public  class ProductsWithBrandsAndTypesSpecification : BaseSpefication<Products>
    {
        public ProductsWithBrandsAndTypesSpecification(ProductsSpecParams productsParams)
            : base(x =>
             (string.IsNullOrEmpty(productsParams.Search)|| x.Name.ToLower()
             .Contains(productsParams.Search))&&    
             (!productsParams.BrandId.HasValue || x.ProductBrandId == productsParams.BrandId) &&
             (!productsParams.TypeId.HasValue || x.ProductTypeId == productsParams.TypeId)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(p => p.Name);
            AddPagination(productsParams.PageSize * (productsParams.PageIndex - 1), productsParams.PageSize);


            if (!string.IsNullOrEmpty(productsParams.Sort))
            {
               switch (productsParams.Sort.ToLower())
            {
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                default:
                        AddOrderBy(p => p.Name);
                    break;
            }
          }
        }

        public ProductsWithBrandsAndTypesSpecification(int id) : base(x => x.ID == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
