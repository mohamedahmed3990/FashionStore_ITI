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
        public IColorRepository ColorRepository { get;}
        public ISizeRepository SizeRepository { get;}
        public IParentCategoryRepository ParentCategoryRepository { get;}
        public ISubCategoryRepository SubCategoryRepository { get;}

        public IProductVariantRepository ProductVariantRepo { get;}
        public IOrderRepository OrderRepo { get;}

        Task<int> SaveChangesAsync();
    }
}
