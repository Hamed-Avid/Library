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
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.Trusteeship.Update
{
    [Scenario("تحویل کتاب امانت داده شده بعد از تاریخ برگشت")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly TrusteeService sut;
        private Person person;
        private Book book;
        private Trustee trustee;
        private Func<Task> expect;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {

            context = CreateDataContext();
            sut = TrusteeFactory.CreateService(context);
        }

        [Given("تنها یک کتاب با عنوان *سووشون* به عضوی با نام *حامد آوید*" +
            " با تاریخ برگشت* 07/07/2021*  امانت داده شده وجود دارد")]
        private void Given()
        {
            person = new PersonBuilder().
               WithFirstName("حامد").
               WithLastName("آوید").
               Build(context);
            book = new BookBuilder().
               WithTitle("سووشون").
               Build(context);
            trustee = TrusteeFactory.GenerateTrustee(person.Id, book.Id, new DateTime(2021, 01, 07), context);
        }

        [When("کتابی با عنوان *سووشون* و عضوی با نام *حامد آوید  " +
            "امانت در تاریخ *09/07/2021* تحویل داده شود")]
        private void When()
        {
            expect = () => sut.Update(trustee.Id);
        }

        [Then("در فهرست امانتداری تنها یک کتاب امانت داده شده با" +
            " عنوان *سووشون* به عضو کتابخانه با نام *حامد آوید* با " +
            "تاریخ برگشت *07/07/2021* و تاریخ تحویل امانت *09/07/2021*  وجود داشته باشد")]
        [And("خطای *تاریخ تحویل بعد از تاریخ برگشت میباشد* نمایش داده شود")]
        private void Then()
        {
            expect.Should().ThrowExactly<DelivaryDateItsPastFromReturnDateException>();
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
