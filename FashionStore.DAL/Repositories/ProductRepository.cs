using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products
            .Include(p => p.ProductVariants)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(
    string? colorName,
    string? sizeName,
    string? subCategoryName,
    string? categoryName,
    string? sortBy = "lowest price")
        {
            var query = _context.Products
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Size)
            .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.ParentCategory)
            .AsQueryable();

            // Filter by Color Name
            if (!string.IsNullOrEmpty(colorName))
            {
                var colorId = await _context.Colors
                    .Where(c => c.Name.ToLower() == colorName.ToLower())
                    .Select(c => c.Id)
                    .FirstOrDefaultAsync();

                if (colorId != 0)
                    query = query.Where(p => p.ProductVariants.Any(pv => pv.ColorId == colorId));
            }

            // Filter by Size Name
            if (!string.IsNullOrEmpty(sizeName))
            {
                var sizeId = await _context.Sizes
                    .Where(s => s.Name.ToLower() == sizeName.ToLower())
                    .Select(s => s.Id)
                    .FirstOrDefaultAsync();

                if (sizeId != 0)
                    query = query.Where(p => p.ProductVariants.Any(pv => pv.SizeId == sizeId));
            }

            // Filter by SubCategory Name
            if (!string.IsNullOrEmpty(subCategoryName))
            {
                query = query.Where(p => p.SubCategory.Name.ToLower() == subCategoryName.ToLower());
            }

            // Filter by Category Name (ParentCategory)
            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(p => p.SubCategory.ParentCategory.Name.ToLower() == categoryName.ToLower());
            }

            // Sorting
            switch (sortBy?.ToLower())
            {
                case "lowest price":
                    query = query
                        .OrderBy(p => p.ProductVariants
                            .OrderBy(pv => pv.Price)
                            .Select(pv => pv.Price)
                            .FirstOrDefault());
                    break;

                case "highest price":
                    query = query
                        .OrderByDescending(p => p.ProductVariants
                            .OrderByDescending(pv => pv.Price)
                            .Select(pv => pv.Price)
                            .FirstOrDefault());
                    break;

                default:
                    query = query.OrderBy(p => p.ProductName);
                    break;
            }

            var products = await query.ToListAsync();

            // Apply the filtering on the ProductVariants
            foreach (var product in products)
            {
                product.ProductVariants = product.ProductVariants
                    .Where(pv =>
                        (string.IsNullOrEmpty(colorName) || pv.Color.Name.ToLower() == colorName.ToLower()) &&
                        (string.IsNullOrEmpty(sizeName) || pv.Size.Name.ToLower() == sizeName.ToLower()))
                    .ToList();
            }

            // Remove products with no matching variants after filter
            products = products.Where(p => p.ProductVariants.Any()).ToList();

            return products;
        }

    }
}
