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
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly AppDbContext _context;

        public ProductVariantRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ProductVariant?> GetProductVariantAsync(int id)
        {
            return await _context.ProductVariants
            .Include(p => p.Product) 
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
