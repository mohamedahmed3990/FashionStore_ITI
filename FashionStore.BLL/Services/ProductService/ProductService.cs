using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL;
using FashionStore.DAL.Entities.ProductAggregate;

namespace FashionStore.BLL.Services.ProductService
{
    public class ProductService :IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(int? colorId, int? sizeId, int? subCategoryId, int? categoryId, string sort)
        {
            return await _unitOfWork.ProductRepo.GetFilteredProductsAsync(
            colorId, sizeId, subCategoryId, categoryId, sort);
        }
    }
}
