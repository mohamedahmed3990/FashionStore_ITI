using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.DAL;
using FashionStore.DAL.Entities.ProductAggregate;
using FashionStore.DAL.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace FashionStore.BLL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ProductService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> FilterProductsAsync(string? colorName,
        string? sizeName,
        string? subCategoryName,
        string? categoryName,
        string? sortBy)
        {
            return await _unitOfWork.ProductRepo.GetFilteredProductsAsync(colorName, sizeName, subCategoryName, categoryName, sortBy);
        }
        public async Task<ProductDetailsDTO> GetProductDetailsAsync(int id)
        {
            var product = await _unitOfWork.ProductRepo.GetProductWithDetailsAsync(id);

            if (product == null) return null;

            return new ProductDetailsDTO
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPicture = $"{_configuration["local"]}/Images/{product.ProductPicture}",
                SubCategoryName = product.SubCategory?.Name,
                CategoryName = product.SubCategory?.ParentCategory?.Name,
                ProductVariants = product.ProductVariants.Select(pv => new ProductVariantDTO
                {
                    Id = pv.Id,
                    Color = new ColorDTO
                    {
                        Id = pv.Color.Id,
                        Name = pv.Color.Name,
                        Hexa = pv.Color.Hexa
                    },
                    Size = new SizeDTO
                    {
                        Id = pv.Size.Id,
                        Name = pv.Size.Name
                    },
                    Price = pv.Price
                }).ToList()
            };
        }


    }
}
