namespace Library.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public byte MinAge { get; set; }
        public byte? MaxAge { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
