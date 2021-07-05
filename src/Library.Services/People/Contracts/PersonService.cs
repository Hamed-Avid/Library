using System.Threading.Tasks;

namespace Library.Services.People.Contracts
{
    public interface PersonService
    {
        Task<int> Add(CreatePersonDto dto);
    }
}
