using Library.Entities;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Categories;
using Library.Services.Books;

namespace Library.Test.Tools.Books
{
    public static class BookFactory
    {
        public static BookAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFBookRepository(context);
            var category = new EFCategoryRepository(context);
            return new BookAppService(repository, unitOfWork,category);
        }
        public static Book GenerateBook(EFDataContext context)
        {
            var book = new Book
            {
                Title = "dummy_title",
                Writer = "dummy",
                MinAge = 7,
                MaxAge = 80,
                Category = new Category { Title = "dummy_title" }
            };
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }
    }
}
