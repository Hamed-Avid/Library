using Library.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookService
    {
        Task<int> Add(CreateBookDto dto);
        Task Update(int id, UpdateBookDto dto);
        Task<List<GetBookDto>> GetByCategory(int categoryId);
    }
}
