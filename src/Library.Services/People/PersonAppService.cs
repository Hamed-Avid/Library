using Library.Services.People.Contracts;

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

    }
}
