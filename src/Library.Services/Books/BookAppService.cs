using Library.Services.Books.Contracts;

namespace Library.Services.Books
{
    public class BookAppService : BookService
    {
        private readonly BookRepository _repository;
        private readonly UnitOfwork _unitOfwork;
        public BookAppService(BookRepository repository, UnitOfwork unitOfwork)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
        }

    }
}
