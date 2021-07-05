using FluentAssertions;
using Library.Entities;
using Library.Persistence.EF;
using Library.Services.Books.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.Books;
using Library.Test.Tools.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Books.Update
{
    [Scenario("ویرایش مشخصات یک کتاب")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly BookService sut;
        private Book book;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = BookFactory.CreateService(context);
        }

        [Given(" تنها یک کتاب با عنوان *سووشون* و نویسنده *سیمین دانشور* و " +
            "با رده سنی *16تا80* سال در دسته بندی *رمان* وجود دارد")]
        private void Given()
        {
            var category = CategoryFactory.GenerateCategory(context, "رمان");
            book = new BookBuilder().
               WithTitle("سووشون").
               WithWriter("سیمین دانشور").
               WithMinAge(16).
               WithMaxAge(80).
               WithCategoryId(category.Id).
               Build(context);
        }

        [When("عنوان کتاب را به *جزیره سرگردانی* ویرایش میکنم")]
        private async Task When()
        {
            var dto = new UpdateBookDto { Title = "جزیره سرگردانی" };

            await sut.Update(book.Id,dto);
        }

        [Then("باید در فهرست کتابها تنها یک کتاب با عنوان* جزیره سرگردانی* و نویسنده" +
            "* سیمین دانشور* و با رده سنی*16تا80* سال در دسته بندی * رمان * وجود داشته باشد")]
        private void Then()
        {
            var Expected = context.Books.Include(_ => _.Category).Single(_ => _.Id == book.Id);

            Expected.Title.Should().Be("جزیره سرگردانی");
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

