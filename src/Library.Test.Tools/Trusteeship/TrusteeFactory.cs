using Library.Entities;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Persistence.EF.People;
using Library.Persistence.EF.Trusteeship;
using Library.Services.Trusteeship;
using System;

namespace Library.Test.Tools.Trusteeship
{
    public class TrusteeFactory
    {
        public static TrusteeAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFTrusteeRepository(context);
            var person = new EFPersonRepository(context);
            var book = new EFBookRepository(context);
            return new TrusteeAppService(repository, unitOfWork, person, book);
        }

        public static Trustee GenerateTrustee(int personId, int bookId, DateTime returnDate,EFDataContext context)
        {
            var trustee = new Trustee
            {
                PersonId = personId,
                BookId = bookId,
                ReturnDate = returnDate
            };
            context.Trusteeship.Add(trustee);
            context.SaveChanges();
            return trustee;
        }
    }
}
