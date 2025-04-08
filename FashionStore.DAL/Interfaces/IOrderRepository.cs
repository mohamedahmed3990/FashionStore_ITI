﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Migrations;

namespace FashionStore.DAL.Interfaces
{
    public interface IOrderRepository : IGenericRepo<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId);
    }
}
