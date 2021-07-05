using Library.Entities;
using Library.Services.People.Contracts;
using System.Threading.Tasks;

namespace Library.Services.People
{
    public class PersonAppService : PersonService
    {
        private readonly PersonRepository _repository;
        private readonly UnitOfwork _unitOfwork;
        public PersonAppService(PersonRepository repository, UnitOfwork unitOfwork)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
        }

        public async Task<int> Add(CreatePersonDto dto)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Address = dto.Address
            };
            _repository.Add(person);
            await _unitOfwork.Complete();
            return person.Id;
        }
    }
}
