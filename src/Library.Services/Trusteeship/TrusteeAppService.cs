using Library.Entities;
using Library.Services.Books.Contracts;
using Library.Services.People.Contracts;
using Library.Services.Trusteeship.Contracts;
using Library.Services.Trusteeship.Exceptions;
using System;
using System.Threading.Tasks;

namespace Library.Services.Trusteeship
{
    public class TrusteeAppService : TrusteeService
    {
        private readonly TrusteeRepository _repository;
        private readonly UnitOfwork _unitOfwork;
        private readonly PersonRepository _person;
        private readonly BookRepository _book;
        public TrusteeAppService(TrusteeRepository repository, UnitOfwork unitOfwork,
            PersonRepository person, BookRepository book)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
            _person = person;
            _book = book;
        }

        public async Task<int> Add(CreateTrusteeDto dto)
        {
            var book = await _book.GetById(dto.BookId);
            GuardAgainstInvalidBookId(book);
            var person = await _person.GetById(dto.PersonId);
            GuardAgainstInvalidPesronId(person);
            var personAge = DateTime.Now.Year - person.BirthDate.Year;
            GuardAgainstInvalidPersonAge(personAge, book);
            var trustee = new Trustee
            {
                BookId = dto.BookId,
                PersonId = dto.PersonId,
                ReturnDate = dto.ReturnDate
            };
            _repository.Add(trustee);
            await _unitOfwork.Complete();
            return trustee.Id;
        }

        public async Task Update(int id)
        {
            var trustee = await _repository.GetById(id);
            trustee.DeliveryDate = DateTime.Now.ToUniversalTime();
            await _unitOfwork.Complete();
            if (trustee.DeliveryDate > trustee.ReturnDate)
                throw new DelivaryDateItsPastFromReturnDateException();
        }

        private static void GuardAgainstInvalidPersonAge(int personAge, Book book)
        {
            if (personAge < book.MinAge || personAge > book.MaxAge)
                throw new PesronAgeNotValidException();
        }

        private static void GuardAgainstInvalidBookId(Book book)
        {
            if (book == null)
            {
                throw new BookIdInvalidException();
            }
        }

        private static void GuardAgainstInvalidPesronId(Person person)
        {
            if (person == null)
            {
                throw new PersonIdInvalidException();
            }
        }
    }
}
