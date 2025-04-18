using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionStore.DAL.Entities.ProductAggregate;

namespace FashionStore.DAL.Entities
{
    public class Card
    {
        public string? AppUserId { get; set; }
        public AppUser? User { get; set; }

        
        public int ProductId { get; set; }

        public Product? Product { get; set; }
        public int ProductQuantity { get; set; }
    }
}
