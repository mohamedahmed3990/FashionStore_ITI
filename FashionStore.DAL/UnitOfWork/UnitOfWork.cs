using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepo { get; }
        public IProductVariantRepository ProductVariantRepo { get; }
        public IOrderRepository OrderRepo { get; }

        public UnitOfWork(IProductRepository productRepo, AppDbContext context,
                          IProductVariantRepository productVariantRepo,
                          IOrderRepository orderRepo)
        {
            ProductRepo = productRepo;
            _context = context;
            ProductVariantRepo = productVariantRepo;
            OrderRepo = orderRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); 
        }
    }
}
