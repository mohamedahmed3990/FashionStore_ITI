using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.ProductAggregate;

namespace FashionStore.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetProductAsync(int id);
    }
}
