using Library.Services.Books.Contracts;

namespace Library.Persistence.EF.Books
{
    public class EFBookRepository : BookRepository
    {
        private readonly EFDataContext _context;
        public EFBookRepository(EFDataContext context) => _context = context;

    }
}
