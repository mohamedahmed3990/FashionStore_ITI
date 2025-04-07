using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }

        public ProductItemOrdered(string productName, string productPicture, string productColor, string productSize)
        {
            ProductName = productName;
            ProductPicture = productPicture;
            ProductColor = productColor;
            ProductSize = productSize;
        }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductPicture { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }
    }
}
