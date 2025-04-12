using FashionStore.BLL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var colors = await _colorService.GetAllColorsAsync();
                return Ok(colors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetColorById(int id)
        {
            var color = await _colorService.GetColorByIdAsync(id);
            if (color == null)
                return NotFound();

            return Ok(color);
        }

    }
}
