using FluentAssertions;
using Library.Entities;
using Library.Persistence.EF;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.Books;
using Library.Test.Tools.Categories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Books.Add
{
    [Scenario("ثبت یک کتاب جدید")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private int bookId;
        private Category category;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }

        [Given("تنها یک دسته بندی با عنوان *رمان* وجود دارد")]
        private void Given() => category = CategoryFactory.GenerateCategory(context, "رمان");

        [When("کتابی با عنوان *سووشون* و نویسنده *سیمین دانشور* و" +
            " با رده سنی *16تا80* سال اضافه می نمایم")]
        private async Task When()
        {
            var dto = new BookDtoBuilder()
                .WithTitle("سووشون")
                .WithWriter("سیمین دانشور")
                .WithMinAge(16)
                .WithMaxAge(80)
                .WithCategoryId(category.Id)
                .Build();

            bookId = await sut.Add(dto);
        }

        [Then("درفهرست کتابها باید تنها یک کتاب با عنوان *سووشون* و نویسنده *سیمین دانشور* و" +
            " با رده سنی *16تا80* سال در دسته بندی * رمان * وجود داشته باشد")]
        private void Then()
        {
            var Expected = context.Books.Include(_ => _.Category).Single(_ => _.Id == bookId);

            Expected.Title.Should().Be("سووشون");
            Expected.Writer.Should().Be("سیمین دانشور");
            Expected.MinAge.Should().Be(16);
            Expected.MaxAge.Should().Be(80);
            Expected.Category.Title.Should().Be("رمان");
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
