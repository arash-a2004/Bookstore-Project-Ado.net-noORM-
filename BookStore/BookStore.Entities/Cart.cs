namespace BookStore.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BookName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
    }
}
