using FluentAssertions;
using Library.Entities;
using Library.Persistence.EF;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Services.Trusteeship.Contracts;
using Library.Services.Trusteeship.Exceptions;
using Library.Test.Tools.Books;
using Library.Test.Tools.People;
using Library.Test.Tools.Trusteeship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Trusteeship.Add
{
    [Scenario("ثبت امانت به یکی از اعضا کتابخانه با سن خارج از رده سنی کتاب")]
    public class FaildWhenAgeNotValid : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly TrusteeService sut;
        private Person person;
        private Book book;
        private Func<Task> expexted;
        public FaildWhenAgeNotValid(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = TrusteeFactory.CreateService(context);
        }

        [Given("یک عضو با نام *حامد آوید* و سن *2009/09/09* سال وجود دارد")]
        [And("یک کتاب با عنوان *سووشون* و با رده سنی *16تا80* در فهرست کتابها وجود دارد")]
        private void Given()
        {
            person = new PersonBuilder().
               WithFirstName("حامد").
               WithLastName("آوید").
               WithBirthDate(new DateTime(2009, 09, 09)).
               Build(context);
            book = new BookBuilder().
               WithTitle("سووشون").
               WithMinAge(16).
               WithMaxAge(80).
               Build(context);
        }

        [When("یک کتاب با عنوان *سووشون* به عضوی" +
            " با نام *حامد آوید* با تاریخ برگشت *07/07/2021* امانت داده شود")]
        private void When()
        {
            var dto = new CreateTrusteeDto
            {
                BookId = book.Id,
                PersonId = person.Id,
                ReturnDate = new DateTime(2021, 07, 07)
            };
            expexted = () => sut.Add(dto);
        }

        [Then("نباید هیچ امانتی به فهرست امانتها اضافه گردد")]
        [And("خطای *سن عضو کتابخانه خارج از رده سنی کتاب میباشد* نمایش داده شود")]
        private void Then()
        {
            var expectedcount = context.Trusteeship.Count();
            expectedcount.Should().Be(0);
            expexted.Should().ThrowExactly<PesronAgeNotValidException>();
        }

        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then()
                );
        }
    }
}
