using FluentAssertions;
using Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Categories;
using Library.Services.Categories;
using Library.Services.Categories.Contracts;
using Library.Services.Categories.Exceptions;
using Library.Test.Tools.Categories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.Categories
{
    public class CategoryServiceTests
    {
        private CategoryService sut;
        private EFDataContext context;

        public CategoryServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            var repository = new EFCategoryRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            sut = new CategoryAppService(repository, unitOfWork);
        }

        [Theory]
        [InlineData("رمان")]
        public void Add_throw_exception_when_duplicate_category_title_found(string DuplicateCategoryTitle)
        {
            var category = CategoryFactory.GenerateCategory(context,DuplicateCategoryTitle);

            var dto = new CreateCategoryDto { Title = category.Title };
            Func<Task> expected = () => sut.Add(dto);

            expected.Should().ThrowExactly<DuplicateTitleNotValidException>();
        }
    }
}
