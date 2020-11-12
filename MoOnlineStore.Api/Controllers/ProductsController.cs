using System.Collections.Generic;

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoOnlineStore.Api.Errors;
using MoOnlineStore.Api.Helpers;
using MoOnlineStore.Api.Pages;
using MoOnlineStore.Core.DTO;
using MoOnlineStore.Core.EntityClasses;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Core.specifications;
using MoOnlineStore.Core.Specifications;

namespace MoOnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
        {

        private readonly IGenericRepository<Products> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Products> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo,IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;   
        }

        [Cashed(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DTOProducts>>>GetProducts(
            [FromQuery]ProductsSpecParams ProductsParams)
        {
             var spec = new ProductsWithBrandsAndTypesSpecification(ProductsParams);
             var countSpec = new ProductsWithFiltersForCountSpecification(ProductsParams);
             var products = await _productsRepo.ListWithSpecAsync(spec);
             var totalcount = await _productsRepo.CountAsync(countSpec);
             var data = _mapper.Map<IReadOnlyList<Products>, IReadOnlyList<DTOProducts>>(products);
             if (data == null)
            {
             return NotFound(new ApiResponse(404));
            }
            return Ok(new Pagination<DTOProducts>(ProductsParams.PageSize,ProductsParams.PageIndex,totalcount,data));
          
        }
        [Cashed(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<DTOProducts>>> GetProduct(int id)
        {
            var spec = new ProductsWithBrandsAndTypesSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if(product == null)
            {
             return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Products, DTOProducts>(product));
        }
        [Cashed(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<DTOProductBrand>>> GetProductBrands()
        {
            var productBrand = await _productBrandRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<DTOProductBrand>>(productBrand));
        }
        [Cashed(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<DTOProductType>>> GetProductTypes()
        {
            var productType = await _productTypeRepo.ListAllAsync();
            return Ok(_mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<DTOProductType>>(productType));
        }
      
    }
}
