using System.Collections.Generic;
using NotebookApp;
using NUnit.Framework;


namespace _NotebookApp.Tests
{
    [TestFixture]
    class NotebookTests
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            var note1 = new Note()
            {
                Name = "1",
                Surname = "2",
                Lastname = "",
                PhoneNumber = "12345",
                Birthdate = "",
                Organization = "qwer",
                Position = "qwww",
                Country = "qqwwwww",
                OtherNotes = ""
            };
            var note2 = new Note()
            {
                Name = "12",
                Surname = "22",
                Lastname = "",
                PhoneNumber = "12345",
                Birthdate = "",
                Organization = "qwer",
                Position = "qwww",
                Country = "qqwwwww",
                OtherNotes = ""
            };
            var note3 = new Note()
            {
                Name = "1111",
                Surname = "2",
                Lastname = "",
                PhoneNumber = "12345",
                Birthdate = "",
                Organization = "qwer",
                Position = "qwww",
                Country = "qqwwwww",
                OtherNotes = ""
            };
            var note4 = new Note()
            {
                Name = "1",
                Surname = "2222",
                Lastname = "",
                PhoneNumber = "12345",
                Birthdate = "",
                Organization = "qwer",
                Position = "qwww",
                Country = "qqwwwww",
                OtherNotes = ""
            };
            Notebook.CreateNote(note1);
            Notebook.CreateNote(note2);
            Notebook.CreateNote(note3);
            Notebook.CreateNote(note4);
        }

        [TestCase("Name", "1", ExpectedResult = 2)]
        [TestCase("Name", "10", ExpectedResult = 0)]
        [TestCase("Name", "12", ExpectedResult = 1)]
        [TestCase("Surname", "2", ExpectedResult = 2)]
        [TestCase("Surname", "143242", ExpectedResult = 0)]
        public int CheckFinder(string searchParameter, string searchInfo)
        {
            var x = Notebook.FindNote(searchParameter, searchInfo).Count;
            return x;
        }
        
    }
}