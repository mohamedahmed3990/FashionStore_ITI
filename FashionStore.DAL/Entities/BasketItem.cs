namespace FashionStore.DAL.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        public string Color {  get; set; }

        public string Size { get; set; }
        public decimal Price { get; set; }
         
        public int Quantity { get; set; }

    }
}