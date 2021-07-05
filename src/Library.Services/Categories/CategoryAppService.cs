using Library.Entities;
using Library.Services.Categories.Contracts;
using Library.Services.Categories.Exceptions;
using System.Threading.Tasks;

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

        public async Task<int> Add(CreateCategoryDto dto)
        {
            await GourdAgainstDuplicateCategoryTitle(dto);
            var category = new Category { Title = dto.Title };
            _repository.Add(category);
            await _unitOfwork.Complete();
            return category.Id;
        }

        private async Task GourdAgainstDuplicateCategoryTitle(CreateCategoryDto dto)
        {
            if (await _repository.IsExistByTitle(dto.Title))
                throw new DuplicateTitleNotValidException();
        }
    }
}
