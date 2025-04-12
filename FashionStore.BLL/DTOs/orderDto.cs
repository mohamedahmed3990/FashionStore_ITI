using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.OrderAggregate;

namespace FashionStore.BLL.DTOs
{
    public class orderDto
    {
        public string BasketId { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public decimal ShippingFee { get; set; }

    }
}
