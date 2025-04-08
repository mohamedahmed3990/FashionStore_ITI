using FashionStore.DAL.Context;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL.Repositories
{
    public class SizeRepository : GenericRepo<Entities.ProductAggregate.Size>, ISizeRepository
    {
        private readonly AppDbContext _context;

        public SizeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
