using FluentAssertions;
using Library.Entities;
using Library.Persistence.EF;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.Books;
using Library.Test.Tools.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Books.Get
{
    [Scenario("نمایش فهرست کتابهای یک دسته بندی")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private Book book;
        private List<GetBookDto> expected;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }

        [Given("تنها یک کتاب با عنوان *سووشون* در دسته بندی *رمان* وجود دارد")]
        private void Given()
        {
            var category = CategoryFactory.GenerateCategory(context, "رمان");
            book = new BookBuilder().
                WithTitle("سووشون").
                WithCategoryId(category.Id).
                Build(context);
        }

        [When("در دسته بندی *رمان* فهرست کتابها را مشاهده میکنم")]
        private async Task When()
        {
            expected = await sut.GetByCategory(book.CategoryId);
        }

        [Then("در فهرست کتابهای دسته بندی *رمان* " +
            "تنها یک کتاب با عنوان *سووشون*  باید وجود داشته باشد")]
        private void Then()
        {
            expected.Count.Should().Be(1);
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then()
                );
        }
    }
}
