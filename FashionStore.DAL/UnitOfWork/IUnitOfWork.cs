using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Interfaces;

namespace FashionStore.DAL
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
