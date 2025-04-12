namespace FashionStore.BLL.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductPicture { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}