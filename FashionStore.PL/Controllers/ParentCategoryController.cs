using FashionStore.BLL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentCategoryController : ControllerBase
    {
        private readonly IParentCategoryService _parentCategoryService;

        public ParentCategoryController(IParentCategoryService parentCategoryService)
        {
            _parentCategoryService = parentCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllParentCategories()
        {
            try
            {
                var ParentCategories = await _parentCategoryService.GetAllParentCategoryAsync();
                return Ok(ParentCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParentCategoryById(int id)
        {
            var ParentCategory = await _parentCategoryService.GetParentCategoryByIdAsync(id);
            if (ParentCategory == null)
                return NotFound();

            return Ok(ParentCategory);
        }
    }
}
