using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.ProductService
{
    public class SubCategoryService :ISubCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SubCategoryDto>> GetAllSubCategoryAsync()
        {
            var SubCategories = await _unitOfWork.SubCategoryRepository.GetAllAsync();
            return SubCategories.Select(c => new SubCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<SubCategoryReadByIdDto> GetSubCategoryByIdAsync(int id)
        {
            var SubCategory = await _unitOfWork.SubCategoryRepository.GetByIdAsync(id);
            if (SubCategory == null)
                return null;

            return new SubCategoryReadByIdDto
            {
                //Id = color.Id,
                Name = SubCategory.Name
            };
        }
    }
}
