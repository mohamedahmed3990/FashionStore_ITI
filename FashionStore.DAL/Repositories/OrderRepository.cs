using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities.OrderAggregate;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Repositories
{
    public class OrderRepository : GenericRepo<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public Task<Order?> GetOrderByUserAsync(int id, string buyerEmail)
        {
            var order = _context.Set<Order>().Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id && o.BuyerEmail == buyerEmail);
            if (order is null) return null;

            return order;
        }

        public Task<List<Order>> GetOrdersByUserAsync(string buyerEmail)
        {
            var orders =  _context.Set<Order>().Where(o => o.BuyerEmail == buyerEmail).Include(o => o.Items).OrderByDescending(o => o.OrderDate).ToListAsync();

            return orders;
        }

       
    }
}
