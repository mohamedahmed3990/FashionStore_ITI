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

        public ProductItemOrdered(int id, string productName, string productPicture, string productColor, string productSize)
        {
            ProductId = id;
            ProductName = productName;
            ProductPicture = productPicture;
            ProductColor = productColor;
            ProductSize = productSize;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductPicture { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }
    }
}
