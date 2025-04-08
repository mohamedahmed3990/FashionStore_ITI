using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.BLL.DTOs.ProductDto;

namespace FashionStore.BLL.Services.ProductService
{
    public interface IColorService
    {
        public Task<IEnumerable<ColorDTO>> GetAllColorsAsync();
        Task<ColorReadByIdDto> GetColorByIdAsync(int id);

    }
}
