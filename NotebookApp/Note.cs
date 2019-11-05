using System;
using System.Collections.Generic;
using System.Text;

namespace NotebookApp
{
    class Note
    {
        private string _name;
        private string _surname;
        private string _lastname;
        private ulong _phoneNumber;
        private string _country;
        private DateTime _birthdate;
        private string _organization;
        private string _position;
        private string _other;

        public string Name
        {
            get => _name;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    Console.Write("Введите имя: ");
                    value = Console.ReadLine();
                }

                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    Console.Write("Введите фамилию: ");
                    value = Console.ReadLine();
                }

                _surname = value;
            }
        }

        public string Lastname
        {
            get => _lastname;
            set
            {
                if (value == "") value = "Отсуствует";
                _lastname = value;
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber.ToString();
            set
            {
                ulong number;
                while (ulong.TryParse(value, out number))
                {
                    Console.WriteLine("Номер состоит только из цифр!!");
                    Console.Write("Введите телефон: ");
                    value = Console.ReadLine();
                }

                _phoneNumber = number;
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                while (value == "")
                {
                    Console.WriteLine("Поле не должно быть пустым!!!");
                    Console.Write("Введите страну: ");
                    value = Console.ReadLine();
                }

                _country = value;
            }
        }

        public string Birthdate
        {
            get => _birthdate == DateTime.MinValue ? "Отсутствует" : _birthdate.ToString();

            set
            {
                if (value == "") _birthdate = DateTime.MinValue;
                var date = new DateTime();
                while (DateTime.TryParse(value, out date))
                {
                    value = Console.ReadLine();
                }

                _birthdate = date;
            }
        }

        public string Organization
        {
            get => _organization;
            set
            {
                if (value == "") value = "Отсуствует";

                _organization = value;
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                if (value == "") value = "Отсуствует";

                _position = value;
            }
        }

        public string OtherNotes
        {
            get => _other;
            set
            {
                if (value == "") value = "Отсуствует";

                _other = value;
            }
        }
    }
}
