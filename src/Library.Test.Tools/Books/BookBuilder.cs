using Library.Entities;
using Library.Persistence.EF;

namespace Library.Test.Tools.Books
{
    public class BookBuilder
    {
        private Book book = new Book
        {
            Category = new Category { Title = "dummy_title" },
            Title = "dummy_title",
            Writer = "dummy",
            MinAge = 7
        };
        public BookBuilder WithTitle(string title)
        {
            book.Title = title;
            return this;
        }
        public BookBuilder WithWriter(string writer)
        {
            book.Writer = writer;
            return this;
        }
        public BookBuilder WithMinAge(byte minage)
        {
            book.MinAge = minage;
            return this;
        }
        public BookBuilder WithMaxAge(byte maxage)
        {
            book.MaxAge = maxage;
            return this;
        }
        public BookBuilder WithCategoryId(int categoryId)
        {
            book.CategoryId = categoryId;
            book.Category = null;
            return this;
        }
        public Book Build(EFDataContext context)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
    }
}
