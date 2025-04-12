using System.Security.Claims;
using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.OrderService;
using FashionStore.DAL.Entities.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] orderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if(buyerEmail is null) return NotFound();

            var address = new Address
            {
                Country = orderDto.ShippingAddress.Country,
                City = orderDto.ShippingAddress.City,
                AddressDetails = orderDto.ShippingAddress.AddressDetails,
            };
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.ShippingFee, address);

            if (order == null) { return BadRequest(); }
            return Ok(order);
        } 


        [HttpGet]
        public async Task<ActionResult<List<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (buyerEmail is null) { return NotFound(); }

            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser([FromRoute]int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            if (buyerEmail is null) { return NotFound(); }

            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if(order is null) { return NotFound(); }

            return Ok(order);
        }


    }
}
