using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.Generic;

namespace FashionStore.DAL.Interfaces
{
    public interface IProductRepository : IGenericRepo<Product>
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(
         string? colorName,
        string? sizeName,
        string? subCategoryName,
        string? categoryName,
        string? sortBy);

        Task<Product?> GetProductAsync(int id);
    }
}
