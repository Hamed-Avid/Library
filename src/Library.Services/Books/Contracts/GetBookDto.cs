using Library.Entities;

namespace Library.Services.Books.Contracts
{
    public class GetBookDto
    {
        public string CategoryTitle { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public byte MinAge { get; set; }
        public byte? MaxAge { get; set; }
        public int CategoryId { get; set; }
    }
}
