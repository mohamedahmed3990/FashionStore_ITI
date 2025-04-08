using FashionStore.BLL.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            try
            {
                var SubCategories = await _subCategoryService.GetAllSubCategoryAsync();
                return Ok(SubCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var SubCategory = await _subCategoryService.GetSubCategoryByIdAsync(id);
            if (SubCategory == null)
                return NotFound();

            return Ok(SubCategory);
        }
    }
}
