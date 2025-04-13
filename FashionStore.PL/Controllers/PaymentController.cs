using FashionStore.BLL.DTOs;
using FashionStore.BLL.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketID)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketID);
            if (basket is null)
                return NotFound("Basket is not found");
            return Ok(basket);
        }
    }
}
