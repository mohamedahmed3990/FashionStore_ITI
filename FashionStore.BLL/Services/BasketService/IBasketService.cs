using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FashionStore.DAL.Entities;

namespace FashionStore.BLL.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto?> GetBasketAsync(string basketId);

        Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basket);

        Task<bool> DeleteBasketAsync(string basketId);

        Task MigrateBasketAsync(string guestId, string userId);
    }
}
