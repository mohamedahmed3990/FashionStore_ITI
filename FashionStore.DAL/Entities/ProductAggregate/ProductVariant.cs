using System.Drawing;

namespace FashionStore.DAL.Entities.ProductAggregate
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
    }
}