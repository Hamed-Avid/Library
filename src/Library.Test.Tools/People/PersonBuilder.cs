using Library.Entities;
using Library.Persistence.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Test.Tools.People
{
    public class PersonBuilder
    {
        private Person person = new Person
        {
            FirstName = "dummy_name",
            LastName = "dummy_family",
            BirthDate = new DateTime(2000,2,20),
            Address="dummy_address"
        };
        public PersonBuilder WithFirstName(string firstName)
        {
            person.FirstName = firstName;
            return this;
        }
        public PersonBuilder WithLastName(string lastName)
        {
            person.LastName = lastName;
            return this;
        }
        public PersonBuilder WithBirthDate(DateTime birthDate)
        {
            person.BirthDate = birthDate;
            return this;
        }
        public PersonBuilder WithAddress(string address)
        {
            person.Address = address;
            return this;
        }
        public Person Build(EFDataContext context)
        {
            context.People.Add(person);
            context.SaveChanges();
            return person;
        }
    }
}
