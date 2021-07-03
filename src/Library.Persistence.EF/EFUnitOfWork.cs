using Library.Services;
using System.Threading.Tasks;

namespace Library.Persistence.EF
{
    public class EFUnitOfWork : UnitOfwork
    {
        private readonly EFDataContext _context;

        public EFUnitOfWork(EFDataContext context) => _context = context;

        public async Task Complete() => await _context.SaveChangesAsync();
    }
}
