﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Generic;

namespace FashionStore.DAL.Interfaces
{
    public interface IOrderRepository : IGenericRepo<Order>
    {
        Task<List<Order>> GetOrdersByUserAsync(string buyerEmail);
        Task<Order?> GetOrderByUserAsync(int id , string buyerEmail);
        Task<Order?> GetOrderByPaymentIntentId(string paymentIntentId);
    }
}
