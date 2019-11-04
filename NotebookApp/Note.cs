using System;
using System.Collections.Generic;
using System.Text;

namespace NotebookApp
{
    class Note
    {
        public string Name
        {
            get => Name;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    value = Console.ReadLine();
                }

                Name = value;
            }
        }

        public string Surname
        {
            get => Surname;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    value = Console.ReadLine();
                }

                Surname = value;
            }
        }

        public string Lastname
        {
            get => Lastname;
            set
            {
                if (value == "") value = "Отсуствует";

                Lastname = value;
            }
        }

        public string PhoneNumber
        {
            get => PhoneNumber;
            set
            {
                while (long.TryParse(value, out var number))
                {
                    Console.WriteLine("Номер состоит только из цифр!!");
                    value = Console.ReadLine();
                }

                PhoneNumber = value;
            }
        }

        public string Country
        {
            get => Country;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    value = Console.ReadLine();
                }

                Country = value;
            }
        }

        public DateTime? Birthdate
        {
            get => Birthdate;

            set
            {
                if (value.ToString() == "") value = null;

                Birthdate = value;
            }
        }

        public string Organization
        {
            get => Organization;
            set
            {
                if (value == "") value = "Отсуствует";

                Organization = value;
            }
        }

        public string Position
        {
            get => Position;
            set
            {
                if (value == "") value = "Отсуствует";

                Position = value;
            }
        }

        public string OtherNotes
        {
            get => OtherNotes;
            set
            {
                if (value == "") value = "Отсуствует";

                OtherNotes = value;
            }
        }

        public Note(DateTime? Birthday)
        {
            Birthdate = Birthday;
        }
    }
}
