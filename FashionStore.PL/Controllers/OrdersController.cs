using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.OrderService;
using FashionStore.DAL.Entities.OrderAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] orderDto orderDto)
        {
            var address = new Address
            {
                Country = orderDto.ShippingAddress.Country,
                City = orderDto.ShippingAddress.City,
                AddressDetails = orderDto.ShippingAddress.AddressDetails,
            };
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.ShippingFee, address);

            if (order == null) { return BadRequest(); }
            return Ok(order);
        }
    }
}
