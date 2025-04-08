using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;

namespace FashionStore.BLL.Services.ProductService
{
    public interface ISubCategoryService
    {
        public Task<IEnumerable<SubCategoryDto>> GetAllSubCategoryAsync();
        Task<SubCategoryReadByIdDto> GetSubCategoryByIdAsync(int id);
    }
}
