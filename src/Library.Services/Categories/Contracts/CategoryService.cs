using System.Threading.Tasks;

namespace Library.Services.Categories.Contracts
{
    public interface CategoryService
    {
        Task<int> Add(CreateCategoryDto dto);
    }
}
