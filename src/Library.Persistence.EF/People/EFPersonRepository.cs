using Library.Services.People.Contracts;

namespace Library.Persistence.EF.People
{
    public class EFPersonRepository : PersonRepository
    {
        private readonly EFDataContext _context;
        public EFPersonRepository(EFDataContext context) => _context = context;

    }
}
