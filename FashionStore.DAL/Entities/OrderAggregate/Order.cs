using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.OrderAggregate
{
    public class Order
    {
        public Order()
        {
            
        }

        public Order(Guid id,
                     string buyerEmail,
                     Address shippingAddress,
                     decimal shippingFee,
                     ICollection<OrderItem> items,
                     decimal subTotal)
        {
            this.buyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            ShippingFee = shippingFee;
            Items = items;
            SubTotal = subTotal;
        }

        public Guid Id { get; set; } = new Guid();

        public string buyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        public decimal ShippingFee { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + ShippingFee;









    }
}
