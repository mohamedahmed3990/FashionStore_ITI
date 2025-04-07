using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.DTOs.ProductDto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductPicture { get; set; }
        public int SubCategoryId { get; set; }
        public List<ProductVariantDTO> ProductVariants { get; set; }
    }
}
