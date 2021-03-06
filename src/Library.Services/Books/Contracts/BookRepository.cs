using Library.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository
    {
        void Add(Book book);
        Task<Book> GetById(int id);
        Task<List<GetBookDto>> GetByCategory(int categoryId);
    }
}
