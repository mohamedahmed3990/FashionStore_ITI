using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.DAL;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.ProductService
{
    public class ColorService :IColorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ColorDTO>> GetAllColorsAsync()
        {
            var colors = await _unitOfWork.ColorRepository.GetAllAsync();
            return colors.Select(c => new ColorDTO
            {
                Id = c.Id,
                Name = c.Name,
                Hexa = c.Hexa
            }).ToList();
        }
        public async Task<ColorReadByIdDto> GetColorByIdAsync(int id)
        {
            var color = await _unitOfWork.ColorRepository.GetByIdAsync(id); 
            if (color == null)
                return null;

            return new ColorReadByIdDto
            {
                //Id = color.Id,
                Name = color.Name,
                Hexa = color.Hexa
            };
        }

    }
}
