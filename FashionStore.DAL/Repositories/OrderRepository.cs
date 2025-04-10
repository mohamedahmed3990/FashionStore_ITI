using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL.Repositories
{
    public class OrderRepository : GenericRepo<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
