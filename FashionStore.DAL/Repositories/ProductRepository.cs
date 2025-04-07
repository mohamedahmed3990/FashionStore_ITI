using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionStore.DAL.Repositories
{
    public class ProductRepository : GenericRepo<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(
        int? colorId,
        int? sizeId,
        int? subCategoryId,
        int? categoryId,
        string sort)
        {
            var query = _context.Products
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
            .Include(p => p.SubCategory) 
                .ThenInclude(sc => sc.ParentCategory) 
            .AsQueryable();

            if (colorId.HasValue)
                query = query.Where(p => p.ProductVariants.Any(pv => pv.ColorId == colorId));

            if (sizeId.HasValue)
                query = query.Where(p => p.ProductVariants.Any(pv => pv.SizeId == sizeId));

            if (subCategoryId.HasValue)
                query = query.Where(p => p.SubCategoryId == subCategoryId);

            if (categoryId.HasValue)
                query = query.Where(p => p.SubCategory.ParentCategoryId == categoryId);

            if (sort == "price_asc")
                query = query.OrderBy(p => p.ProductVariants.Min(pv => pv.Price));
            else if (sort == "price_desc")
                query = query.OrderByDescending(p => p.ProductVariants.Min(pv => pv.Price));
            else if (sort == "name")
                query = query.OrderBy(p => p.ProductName);

            return await query.ToListAsync();
        }
        }
}
