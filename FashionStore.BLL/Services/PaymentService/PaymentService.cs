using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities;
using FashionStore.DAL.Interfaces;
using FashionStore.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace FashionStore.BLL.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration , IBasketRepository basketRepo , IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string id)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepo.GetBasketAsync(id);
            if (basket == null)
                return null;

            if (basket.Items?.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var productVariant = await _unitOfWork.ProductVariantRepo.GetProductVariantAsync(item.Id);
                    if (item.Price != productVariant.Price)
                        item.Price = productVariant.Price;
                }
            }

            PaymentIntent paymentIntent;
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => (item.Price * 100) * item.Quantity), // we should adding the shipping cost
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "cart"}
                };

                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => (item.Price * 100) * item.Quantity), // we should adding the shipping cost
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);

                
            }
            await _basketRepo.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
