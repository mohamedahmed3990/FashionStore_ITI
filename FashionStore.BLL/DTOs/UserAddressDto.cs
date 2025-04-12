using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.BLL.DTOs
{
    public class UserAddressDto
    {
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty ;
        public string AddressDetails { get; set; } = string.Empty;
    }
}
