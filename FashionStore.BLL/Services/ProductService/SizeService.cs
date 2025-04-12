using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;
using FashionStore.DAL.UnitOfWork;

namespace FashionStore.BLL.Services.ProductService
{
    public class SizeService : ISizeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SizeDTO>> GetAllSizesAsync()
        {
            var sizes = await _unitOfWork.SizeRepository.GetAllAsync();
            return sizes.Select(s => new SizeDTO
            {
                Name = s.Name,
            }).ToList();
        }

        public async Task<SizeReadByIdDto> GetSizeByIdAsync(int id)
        {
            var size = await _unitOfWork.SizeRepository.GetByIdAsync(id);
            if (size == null)
                return null;

            return new SizeReadByIdDto
            {
                //Id = color.Id,
                Name = size.Name,
            };
        }
    }
}
