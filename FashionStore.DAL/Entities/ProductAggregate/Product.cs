using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.ProductAggregate
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductPicture { get; set; }
        public int SubCategoryId { get; set; }

        // Navigation Property to SubCategory
        public SubCategory SubCategory { get; set; }

        // Navigation Property to ProductVariant
        public List<ProductVariant> ProductVariants { get; set; }
    }
}
