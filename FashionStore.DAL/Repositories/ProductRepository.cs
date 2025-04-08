using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductVariants)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
