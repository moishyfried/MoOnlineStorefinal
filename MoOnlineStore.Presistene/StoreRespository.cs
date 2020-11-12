using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoOnlineStore.Infrastructure
{
     public  class StoreRespository 
    {
        private  StoreContext _Context;
        private IMapper _mapper;
        public StoreRespository(StoreContext Context ,IMapper mapper)
        {
            _mapper = mapper;
            _Context = Context;
        }

        public async Task<IReadOnlyList<DTOProductBrand>> GetProductBrandsAsync()
        {
            var productBrand = await _Context.ProductsBrands.ToListAsync();
            return _mapper.Map<IReadOnlyList<ProductBrand>,IReadOnlyList<DTOProductBrand>>(productBrand);
        }

        public async Task<DTOProducts> GetProductByIdAsync(int id)
        {

            var Product = await _Context.Products.Include("ProducType")
                .Include(p => p.ProductBrand).FirstOrDefaultAsync(p => p.ID == id);
            return _mapper.Map<Products, DTOProducts>(Product);
        }

        public async Task<IReadOnlyList<DTOProducts>> GetProductsAsync()
        {
            var products = await _Context.Products.Include(p => p.ProductType)
                .Include(p => p.ProductBrand).ToListAsync();
            return _mapper.Map<IReadOnlyList<Products>,IReadOnlyList<DTOProducts>>(products);
        }

        public async Task<IReadOnlyList<DTOProductType>> GetProductTypesAsync()
        {
            var producttype = await _Context.ProductsTypes.ToListAsync();
            return _mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<DTOProductType>>(producttype);
        }
    }
}
