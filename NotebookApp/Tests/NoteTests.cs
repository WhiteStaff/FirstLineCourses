using System;
using System.Collections.Generic;
using System.Text;
using NotebookApp;
using NUnit.Framework;

namespace _NotebookApp.Tests
{
    [TestFixture]
    class NoteTests
    {
        [TestCase("1212", ExpectedResult = "1212")]
        public string SetName(string name)
        {
            var b = new Note { Name = name };
            return b.Name;
        }

        [TestCase("1212", ExpectedResult = "1212")]
        public string SetSurname(string surname)
        {
            var b = new Note { Surname = surname };
            return b.Surname;
        }

        [TestCase("1212", ExpectedResult = "1212")]
        public string SetCountry(string country)
        {
            var b = new Note { Country = country };
            return b.Country;
        }


        [TestCase("", ExpectedResult = "Отсутствует")]
        [TestCase("1212", ExpectedResult = "1212")]
        public string SetLastname(string lastname)
        {
            var b = new Note { Lastname = lastname };
            return b.Lastname;
        }

        [TestCase("", ExpectedResult = "Отсутствует")]
        [TestCase("1212", ExpectedResult = "1212")]
        public string SetOrganization(string organization)
        {
            var b = new Note { Organization = organization };
            return b.Organization;
        }

        [TestCase("", ExpectedResult = "Отсутствует")]
        [TestCase("1212", ExpectedResult = "1212")]
        public string SetPosition(string position)
        {
            var b = new Note { Position = position };
            return b.Position;
        }

        [TestCase("", ExpectedResult = "Отсутствует")]
        [TestCase("1212", ExpectedResult = "1212")]
        public string SetOtherNotes(string other)
        {
            var b = new Note { OtherNotes = other };
            return b.OtherNotes;
        }

        [TestCase("", ExpectedResult = "Отсутствует")]
        [TestCase("30.03.1967", ExpectedResult = "30.03.1967")]
        public string SetDate(string birthdate)
        {
            var b = new Note { Birthdate = birthdate };
            return b.Birthdate;
        }

    }
}

