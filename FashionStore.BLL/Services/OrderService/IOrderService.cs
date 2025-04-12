using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs;
using FashionStore.DAL.Entities.OrderAggregate;

namespace FashionStore.BLL.Services.OrderService
{
    public interface IOrderService
    {
        Task<OrderToReturnDto?> CreateOrderAsync(string buyerEmail, string basketId,decimal shippingfee, Address shippingAddress);

        Task<List<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);

        Task<OrderToReturnDto?> GetOrderByIdForUserAsync(int orderId, string buyerEmail);    


    }
}
