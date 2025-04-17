using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.BasketService;
using FashionStore.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket([FromRoute]string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            if (basket == null)
            {
                new CustomerBasket(id);
            }
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket([FromBody] CustomerBasketDto basket)
        {
            var basketResult = await _basketService.UpdateBasketAsync(basket);

            if (basketResult is null) { return BadRequest(); };

            return Ok(basketResult);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBasket(string id)
        {
            await _basketService.DeleteBasketAsync(id);
        }

        [HttpPost("migrate/{id}")]
        [Authorize]
        public async Task<ActionResult> MigrateBasket(string id)
        {
            var userId = User?.Identity?.Name;
            if (userId is null) return Unauthorized();

            await _basketService.MigrateBasketAsync(id, userId);
            return Ok();
        }

            
    }
}
