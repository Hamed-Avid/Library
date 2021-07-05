using Library.Entities;
using System.Threading.Tasks;

namespace Library.Services.Categories.Contracts
{
    public interface CategoryRepository
    {
        void Add(Category category);
        Task<bool> IsExistByTitle(string CategoryTitle);
        Task<bool> IsExistById(int categoryId);
    }
}
