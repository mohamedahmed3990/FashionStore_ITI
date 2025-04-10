using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Context;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.Generic;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL.Repositories
{
    public class SubCategoryRepository : GenericRepo<SubCategory>, ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
