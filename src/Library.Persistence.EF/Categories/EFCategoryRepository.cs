using Library.Services.Categories.Contracts;

namespace Library.Persistence.EF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly EFDataContext _context;

        public EFCategoryRepository(EFDataContext context) => _context = context;

    }
}
