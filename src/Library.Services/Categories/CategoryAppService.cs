using Library.Services.Categories.Contracts;

namespace Library.Services.Categories
{
    public class CategoryAppService : CategoryService
    {
        private readonly CategoryRepository _repository;
        private readonly UnitOfwork _unitOfwork;
        public CategoryAppService(CategoryRepository repository, UnitOfwork unitOfwork)
        {
            _repository = repository;
            _unitOfwork = unitOfwork;
        }

    }
}
