using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepo { get;}

        public IProductVariantRepository ProductVariantRepo { get;}

        Task<int> SaveChangesAsync();
    }
}
