using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Text;

namespace NotebookApp
{
    class Note
    {
        private static int id;
        private string _name;
        private string _surname;
        private string _lastname;
        private ulong _phoneNumber;
        private string _country;
        private DateTime _birthdate;
        private string _organization;
        private string _position;
        private string _other;

        public Note()
        {
            Id = id;
            id++;
        }

        public int Id { get; }

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
                if (value == "") value = "Отсутствует";
                _lastname = value;
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber.ToString();
            set
            {
                ulong number;
                while (!ulong.TryParse(value, out number))
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
            get => _birthdate == DateTime.MinValue ? "Отсутствует" : _birthdate.ToString("d");

            set
            {
                var date = new DateTime();
                while (value != "" && !DateTime.TryParse(value, out date))
                {
                    Console.WriteLine("Дата некорректна!!");
                    Console.Write("Введите дату: ");
                    value = Console.ReadLine();
                }
                if (value == "") _birthdate = DateTime.MinValue;
                _birthdate = date;
            }
        }

        public string Organization
        {
            get => _organization;
            set
            {
                if (value == "") value = "Отсутствует";

                _organization = value;
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                if (value == "") value = "Отсутствует";

                _position = value;
            }
        }

        public string OtherNotes
        {
            get => _other;
            set
            {
                if (value == "") value = "Отсутствует";

                _other = value;
            }
        }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Id записи: {Id}");
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Фамилия: {Surname}");
            Console.WriteLine($"Отчество: {Lastname}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Страна: {Country}");
            Console.WriteLine($"Дата рождения: {Birthdate}");
            Console.WriteLine($"Организация: {Organization}");
            Console.WriteLine($"Должность: {Position}");
            Console.WriteLine($"Другая информация: {OtherNotes}");
            Console.WriteLine("------------------------------------");

        }

        public void ShowMainInfo()
        {
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Фамилия: {Surname}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine("****************************");
        }

        
    }
}
