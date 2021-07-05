using FluentAssertions;
using Library.Persistence.EF;
using Library.Services.People.Contracts;
using Library.Services.Tests.Spec.Infrastructure;
using Library.Test.Tools.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Spec.People.Add
{
    [Scenario("ثبت یک عضو جدید به کتابخانه")]
    public class Successful : EFDataContextDatabaseFixture
    {
        private readonly EFDataContext context;
        private readonly PersonService sut;
        private int personId;
        public Successful(ConfigurationFixture configuration) : base(configuration)
        {
            context = CreateDataContext();
            sut = PersonFactory.CreateService(context);
        }

        [Given("هیچ عضوی در کتابخانه وجود ندارد")]
        private void Given() { }

        [When("یک شخص با نام *حامد آوید* و تاریخ تولد *10 / 11 / 1372* و" +
            " آدرس *شیراز بلوار انقلاب* به فهرست اعضای کتابخانه اضافه می کنم")]
        private async Task When()
        {
            var dto = new CreatePersonDto
            {
                FirstName = "حامد",
                LastName = "آوید",
                BirthDate = new DateTime(1994, 1, 30),
                Address = "شیراز بلوار انقلاب"
            };
            personId = await sut.Add(dto);
        }

        [Then("در فهرست اعضای کتابخانه تنها یک شخص با نام *حامد آوید* و" +
            " تاریخ تولد *10 / 11 / 1372* و آدرس *شیراز بلوار انقلاب* باید وجود داشته باشد")]
        private void Then()
        {
            var expected = context.People.Single(_ => _.Id == personId);

            expected.FirstName.Should().Be("حامد");
            expected.LastName.Should().Be("آوید");
            expected.BirthDate.Should().Be(new DateTime(1994, 1, 30));
            expected.Address.Should().Be("شیراز بلوار انقلاب");
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
