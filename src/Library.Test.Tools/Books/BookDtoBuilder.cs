using Library.Entities;
using Library.Services.Books.Contracts;

namespace Library.Test.Tools.Books
{
    public class BookDtoBuilder
    {
        private CreateBookDto Bookdto = new CreateBookDto
        {
            Title = "dummy_title",
            Writer = "dummy",
            MinAge = 7,
            CategoryId = new Category { Title = "dummy_title" }.Id
        };
        public BookDtoBuilder WithTitle(string title)
        {
            Bookdto.Title = title;
            return this;
        }
        public BookDtoBuilder WithWriter(string writer)
        {
            Bookdto.Writer = writer;
            return this;
        }
        public BookDtoBuilder WithMinAge(byte minage)
        {
            Bookdto.MinAge = minage;
            return this;
        }
        public BookDtoBuilder WithMaxAge(byte maxage)
        {
            Bookdto.MaxAge = maxage;
            return this;
        }
        public BookDtoBuilder WithCategoryId(int categoryId)
        {
            Bookdto.CategoryId = categoryId;
            return this;
        }
        public CreateBookDto Build()
        {
            return Bookdto;
        }
    }
}
