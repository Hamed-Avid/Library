using Library.Entities;
using Library.Services.People.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Library.Persistence.EF.People
{
    public class EFPersonRepository : PersonRepository
    {
        private readonly EFDataContext _context;
        public EFPersonRepository(EFDataContext context) => _context = context;

        public void Add(Person person) => _context.People.Add(person);

        public async Task<Person> GetById(int personId) =>
            await _context.People.SingleOrDefaultAsync(_ => _.Id == personId);


    }
}
