using FluentAssertions;
using Library.Entities;
using Library.Persistence.EF;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Services.Trusteeship.Contracts;
using Library.Test.Tools.Books;
using Library.Test.Tools.People;
using Library.Test.Tools.Trusteeship;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Trusteeship.Add
{
    [Scenario("ثبت امانت یک کتاب به یکی از اعضای کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly TrusteeService sut;
        private int trusteeId;
        private Person person;
        private Book book;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = TrusteeFactory.CreateService(context);
        }

        [Given("یک کتاب با عنوان *سووشون* با رده سنی *16تا80* سال وجود دارد.")]
        [And("یک عضو با نام *حامد آوید* و سن *1994/01/30* سال وجود دارد")]
        private void Given()
        {
            book = new BookBuilder().
               WithTitle("سووشون").
               WithMinAge(16).
               WithMaxAge(80).
               Build(context);
            person = new PersonBuilder().
               WithFirstName("حامد").
               WithLastName("آوید").
               WithBirthDate(new DateTime(1994, 1, 30)).
               Build(context);
        }

        [When("یک کتاب با عنوان *سووشون* به " +
            "عضوی با نام *حامد آوید* با تاریخ برگشت *07/07/2021* امانت داده شود")]
        private async Task When()
        {
            var dto = new CreateTrusteeDto
            {
                BookId = book.Id,
                PersonId = person.Id,
                ReturnDate = new DateTime(2021, 7, 7)
            };
            trusteeId = await sut.Add(dto);
        }

        [Then("در فهرست امانتها تنها یک کتاب با عنوان *سووشون* به " +
            "عضوی با نام *حامد آوید* با تاریخ برگشت *07/07/2021* باید وجود داشته باشد")]
        private void Then()
        {
            var expected = context.Trusteeship.Single(_ => _.Id == trusteeId);

            expected.Book.Title.Should().Be("سووشون");
            expected.Person.FirstName.Should().Be("حامد");
            expected.Person.LastName.Should().Be("آوید");
            expected.ReturnDate.Should().Be(new DateTime(2021, 7, 7));
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
