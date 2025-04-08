using FashionStore.BLL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                var sizes = await _sizeService.GetAllSizesAsync();
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var size = await _sizeService.GetSizeByIdAsync(id);
            if (size == null)
                return NotFound();

            return Ok(size);
        }
    }
}
