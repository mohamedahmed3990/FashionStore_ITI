using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.ProductAggregate
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key for ParentCategory
        public int ParentCategoryId { get; set; }

        // Navigation Property to ParentCategory
        public ParentCategory ParentCategory { get; set; }
    }
}
