using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.BLL.Services.ProductService;
using FashionStore.DAL.Entities.ProductAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ProductVariantController(IProductService productService, IConfiguration configuration)
        {
           _productService = productService;
            _configuration = configuration;
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterVariants(
  [FromQuery] string color,
        [FromQuery] string size,
        [FromQuery] string subCategory,
        [FromQuery] string category,
        [FromQuery] string sortBy)
        {
            var products = await _productService.FilterProductsAsync(
         color, size, subCategory, category, sortBy);

            var productDtos = products.Select(p => new ProductDTO
            {
                ProductId=p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                ProductPicture = $"{_configuration["local"]}/Images/{p.ProductPicture}",
                SubCategoryName = p.SubCategory?.Name,
                CategoryName = p.SubCategory?.ParentCategory?.Name,
                ProductVariants = p.ProductVariants.Select(pv => new ProductVariantDTO
                {
                    Id = pv.Id,
                    //ProductId = pv.ProductId, 
                    Price = pv.Price,
                    Color = new ColorDTO
                    {
                        //Id = pv.Color.Id,
                        Name = pv.Color.Name,
                        Hexa = pv.Color.Hexa
                    },
                    Size = new SizeDTO
                    {
                        //Id = pv.Size.Id,
                        Name = pv.Size.Name
                    }
                }).ToList()
            }).ToList();

            return Ok(productDtos);
        }
    }
}
