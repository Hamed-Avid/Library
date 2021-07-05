using Library.Entities;
using Library.Services.Categories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Library.Persistence.EF.Categories
{
    public class EFCategoryRepository : CategoryRepository
    {
        private readonly EFDataContext _context;

        public EFCategoryRepository(EFDataContext context) => _context = context;

        public void Add(Category category) => _context.Categories.Add(category);

        public async Task<bool> IsExistByTitle(string CategoryTitle) =>
            await _context.Categories.AnyAsync(_ => _.Title == CategoryTitle);

        public async Task<bool> IsExistById(int categoryId) =>
            await _context.Categories.AnyAsync(_=>_.Id==categoryId);
    }
}
