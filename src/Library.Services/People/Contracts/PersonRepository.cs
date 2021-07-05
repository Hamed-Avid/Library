using Library.Entities;
using System.Threading.Tasks;

namespace Library.Services.People.Contracts
{
    public interface PersonRepository
    {
        void Add(Person person);
        Task<Person> GetById(int personId);
    }
}
