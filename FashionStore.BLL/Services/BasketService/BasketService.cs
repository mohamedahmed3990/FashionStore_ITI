using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.Common;
using FashionStore.BLL.DTOs;
using FashionStore.BLL.Validators;
using FashionStore.DAL.Entities;
using FashionStore.DAL.Interfaces;
using FluentValidation;
using StackExchange.Redis;

namespace FashionStore.BLL.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly CustomerBasketDtoValidator _validator;

        public BasketService(IBasketRepository basketRepo, CustomerBasketDtoValidator validator)
        {
            _basketRepo = basketRepo;
            _validator = validator;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _basketRepo.DeleteBasketAsync(basketId);
        }

        public async Task<CustomerBasketDto?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null)
            {
                return null;
            }
            var result = MapToCustomerBasketDto(basket);

            return result;
        }

        public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var result =  _validator.Validate(basket);
            if (!result.IsValid)
            {
                throw new BusinessValidationException(result.Errors);
            }


            var customerBasket = MapToCustomerBasket(basket);

            var addedBasket = await _basketRepo.UpdateBasketAsync(customerBasket);

            if (addedBasket == null)
            {
                return null;
            }

            return MapToCustomerBasketDto(addedBasket);
        }


        public async Task MigrateBasketAsync(string guestId, string userId)
        {
            var guestBasket = await _basketRepo.GetBasketAsync(guestId);

            var userBasket = await _basketRepo.GetBasketAsync(userId) ?? new CustomerBasket(userId);

            if (guestBasket != null)
            {
                foreach (var item in guestBasket.Items)
                {
                    var existingItem = userBasket.Items.FirstOrDefault(i => i.Id == item.Id);
                    if (existingItem != null)
                        existingItem.Quantity += item.Quantity;
                    else
                        userBasket.Items.Add(item);
                }

                await _basketRepo.UpdateBasketAsync(userBasket);
                await _basketRepo.DeleteBasketAsync(guestId);
            }
        }

        private CustomerBasket MapToCustomerBasket(CustomerBasketDto customerBasketDto)
        {

            return new CustomerBasket
            {
                Id = customerBasketDto.Id,
                Items = customerBasketDto.Items.Select(i => new BasketItem
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    PictureUrl = i.PictureUrl,
                    Color = i.Color,
                    Price = i.Price,
                    Size = i.Size,
                    Quantity = i.Quantity,
                }).ToList(),
            };
        }
        private CustomerBasketDto MapToCustomerBasketDto(CustomerBasket customerBasket)
        {
            return new CustomerBasketDto
            {
                Id = customerBasket.Id,
                Items = customerBasket.Items.Select(i => new BasketItemDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    PictureUrl = i.PictureUrl,
                    Color = i.Color,
                    Price = i.Price,
                    Size = i.Size,
                    Quantity = i.Quantity,
                }).ToList(),
            };
        }

    }
}
