using FashionStore.BLL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);
            if (product == null) return NotFound();

            return Ok(product);
        }
    }
}
