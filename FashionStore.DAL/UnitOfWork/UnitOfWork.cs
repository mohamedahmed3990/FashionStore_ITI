using FashionStore.DAL.Context;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IProductRepository ProductRepo { get; }
        public UnitOfWork(IProductRepository productRepo,
        AppDbContext context)
        {
            ProductRepo = productRepo;
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); 
        }
    }
}
