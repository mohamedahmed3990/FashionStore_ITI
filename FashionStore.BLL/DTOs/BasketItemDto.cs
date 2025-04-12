namespace FashionStore.BLL.DTOs
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public  string ProductName { get; set; } 

        public  string PictureUrl { get; set; } 

        public  string Color { get; set; } 

        public  string Size { get; set; } 

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}