using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.ProductAggregate;

namespace FashionStore.BLL.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> FilterProductsAsync(
        string? colorName,
        string? sizeName,
        string? subCategoryName,
        string? categoryName,
        string? sortBy
       );
    }
}
