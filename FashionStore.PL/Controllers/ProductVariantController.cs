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

        public ProductVariantController(IProductService productService)
        {
           _productService = productService;
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

            var productDTOs = products.Select(p => new ProductDTO
            {
                //Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                ProductPicture = p.ProductPicture,
                //SubCategoryId = p.SubCategoryId,
                ProductVariants = p.ProductVariants.Select(pv => new ProductVariantDTO
                {
                    //Id = pv.Id,
                    //ProductId = pv.ProductId,
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
                    },
                    Price = pv.Price
                }).ToList()
            }).ToList();

            return Ok(productDTOs);
        }
    }
}
