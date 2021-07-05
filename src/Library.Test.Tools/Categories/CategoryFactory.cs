using Library.Entities;
using Library.Persistence.EF;
using Library.Persistence.EF.Categories;
using Library.Services.Categories;

namespace Library.Test.Tools.Categories
{
    public static class CategoryFactory
    {
        public static CategoryAppService CreateService(EFDataContext context)
        {
            var unitOfWork = new EFUnitOfWork(context);
            var repository = new EFCategoryRepository(context);
            return new CategoryAppService(repository, unitOfWork);
        }
        public static Category GenerateCategory(EFDataContext context,string title = "dummy_title")
        {
            var category = new Category { Title = title };
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }
    }
}
