using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.OrderAggregate
{
    public class OrderItem
    {
        public OrderItem()
        {
            
        }

        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; set; } = new Guid();

        public ProductItemOrdered Product{ get; set; } 

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
