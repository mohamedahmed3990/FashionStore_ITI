using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.OrderAggregate;

namespace FashionStore.BLL.DTOs
{
    public class OrderToReturnDto
    {
        public int Id { get; set; } 

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; } 

        public Address ShippingAddress { get; set; }

        public decimal ShippingFee { get; set; }
        public List<OrderItemDto> Items { get; set; } 

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string PaymentIntentId { get; set; }

    }
}
