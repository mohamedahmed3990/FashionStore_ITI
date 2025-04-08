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
    public class ColorRepository : GenericRepo<Color>, IColorRepository
    {
        private readonly AppDbContext _context;

        public ColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
