using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.ProductService
{
    public class ProductService :IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(string? colorName,
        string? sizeName,
        string? subCategoryName,
        string? categoryName,
        string? sortBy)
        {
            return await _unitOfWork.ProductRepo.GetFilteredProductsAsync(colorName, sizeName, subCategoryName, categoryName, sortBy);
        }
    }
}
