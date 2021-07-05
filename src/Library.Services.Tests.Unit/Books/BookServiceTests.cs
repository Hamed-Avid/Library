using FluentAssertions;
using Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.Categories;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Books
{
    public class BookServiceTests
    {
        private BookService sut;
        private EFDataContext context;

        public BookServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            var repository = new EFBookRepository(context);
            var category = new EFCategoryRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            sut = new BookAppService(repository, unitOfWork,category);
        }

        [Theory]
        [InlineData(0)]
        public void Add_throw_exception_when_duplicate_category_title_found(int InvalidCategoryId)
        {
            var dto = new CreateBookDto { CategoryId=InvalidCategoryId};
            
            Func<Task> expected = () => sut.Add(dto);

            expected.Should().ThrowExactly<InvalidCategoryException>();
        }
    }
}
