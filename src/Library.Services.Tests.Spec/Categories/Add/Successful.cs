using FluentAssertions;
using Library.Persistence.EF;
using Library.Services.Categories.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.Categories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Categories.Add
{
    [Scenario("ثبت یک دسته بندی")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly CategoryService sut;
        private int categoryId;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = CategoryFactory.CreateService(context);
        }

        [Given("هیچ دسته بندی وجود ندارد")]
        private void Given() { }

        [When("یک دسته بندی با عنوان *رمان* اضافه می کنم.")]
        private async Task When()
        {
            var dto = new CreateCategoryDto { Title = "رمان" };
            categoryId = await sut.Add(dto);
        }

        [Then("تنها یک دسته بندی با عنوان *رمان* در فهرست دسته بندی ها باید وجود داشته باشد.")]
        private void Then()
        {
            var expected = context.Categories.Single(_ => _.Id == categoryId);

            expected.Title.Should().Be("رمان");
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
