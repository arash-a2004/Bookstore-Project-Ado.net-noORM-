using BookStore.Entities;

namespace BookStore.web.ViewModels
{
    public class BookwithCategory
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISBNNumber { get; set; }
        public decimal Price { get; set; }
        public string PictureUri { get; set; }
        public BookAuthor Author { get; set; }
        public Store BookStore { get; set; }
        public int Quantity { get; set; } = 1;
        public List<Category> Categories { get; set; }
    }
}
