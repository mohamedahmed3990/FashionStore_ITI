using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.DAL.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string country, string city, string addressDetails)
        {
            Country = country;
            City = city;
            AddressDetails = addressDetails;
        }

        public string Country { get; set; }
        public string City { get; set; }
        public string AddressDetails { get; set; }
    }
}
