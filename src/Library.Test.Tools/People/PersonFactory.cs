using Library.Entities;
using Library.Persistence.EF;
using Library.Persistence.EF.People;
using Library.Services.People;

namespace Library.Test.Tools.People
{
    public static class PersonFactory
    {
        public static PersonAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFPersonRepository(context);
            return new PersonAppService(repository, unitOfWork);
        }
        
    }
}
