using FluentAssertions;
using Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.People;
using Library.Persistence.EF.Trusteeship;
using Library.Services.Trusteeship;
using Library.Services.Trusteeship.Contracts;
using Library.Services.Trusteeship.Exceptions;
using Library.Test.Tools.Books;
using Library.Test.Tools.Trusteeship;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Trusteeship
{
    public class TrusteeServiceTests
    {
        private TrusteeService sut;
        private EFDataContext context;

        public TrusteeServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            var repository = new EFTrusteeRepository(context);
            var person = new EFPersonRepository(context);
            var book = new EFBookRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            sut = new TrusteeAppService(repository, unitOfWork, person, book);
        }

        [Theory]
        [InlineData(0)]
        public void Add_throw_exception_when_bookId_not_found(int InvalidBookId)
        {
            var dto = new CreateTrusteeDto { BookId = InvalidBookId };

            Func<Task> expected = () => sut.Add(dto);

            expected.Should().ThrowExactly<BookIdInvalidException>();
        }

        [Theory]
        [InlineData(0)]
        public void Add_throw_exception_when_personId_not_found(int InvalidPersonId)
        {
            var book = BookFactory.GenerateBook(context);

            var dto = new CreateTrusteeDto { BookId = book.Id, PersonId = InvalidPersonId, ReturnDate=new DateTime(2021, 07, 07) };
            Func<Task> expected = () => sut.Add(dto);

            expected.Should().ThrowExactly<PersonIdInvalidException>();
        }

    }
}
