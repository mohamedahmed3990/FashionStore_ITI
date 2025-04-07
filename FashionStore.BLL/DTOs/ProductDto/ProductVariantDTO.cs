using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.DTOs.ProductDto
{
    public class ProductVariantDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ColorDTO Color { get; set; }
        public SizeDTO Size { get; set; }
        public decimal Price { get; set; }
    }
}
