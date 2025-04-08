using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.ProductService
{
    public class ParentCategoryService :IParentCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParentCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ParentCategoryDto>> GetAllParentCategoryAsync()
        {
            var ParentCategories = await _unitOfWork.ParentCategoryRepository.GetAllAsync();
            return ParentCategories.Select(c => new ParentCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }

        public async Task<ParentCategoryReadByIdDto> GetParentCategoryByIdAsync(int id)
        {
            var ParentCategory = await _unitOfWork.ParentCategoryRepository.GetByIdAsync(id);
            if (ParentCategory == null)
                return null;

            return new ParentCategoryReadByIdDto
            {
                //Id = color.Id,
                Name = ParentCategory.Name
            };
        }
    }
}
