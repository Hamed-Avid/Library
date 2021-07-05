using Library.Entities;
using Library.Services.Books.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;
        public EFBookRepository(EFDataContext context) => _context = context;

        public void Add(Book book) => _context.Books.Add(book);

        public async Task<List<GetBookDto>> GetByCategory(int categoryId)
        {
            //return await _context.Books.Where(_ => _.CategoryId == categoryId).ToListAsync();
            return await (from book in _context.Books
                          join category in _context.Categories on book.CategoryId equals category.Id
                          select new GetBookDto
                          {
                              CategoryTitle = category.Title,
                              Id = book.Id,
                              Title = book.Title,
                              Writer = book.Writer,
                              MinAge = book.MinAge,
                              MaxAge = book.MaxAge
                          }).ToListAsync();
        }
        public async Task<Book> GetById(int bookId) =>
            await _context.Books.SingleOrDefaultAsync(_ => _.Id == bookId);
    }
}
