using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace NotebookApp
{
    class Notebook
    {
        private static List<Note> notes = new List<Note>();
        static void Main(string[] args)
        {
            Companion.Greetings();
            Companion.Info();
            Start();
        }

        static void Start()
        {
            while (true)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        FindNote();
                        break;
                    case "3":
                        EditNote();
                        break;
                    case "4":
                        DeleteNote();
                        break;
                    case "5":
                        ShowAllNotes();
                        break;
                    case "6":
                        Companion.Info();
                        break;
                    case "7":
                        Console.WriteLine("Прощай, друг");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ничего не понимаю! Введи корректную команду");
                        break;
                }
                
            }
        }

        private static void CreateNote()
        {
            var note = new Note();

            Console.Write("Введите фамилию: ");
            note.Surname = Console.ReadLine();

            Console.Write("Введите имя: ");
            note.Name = Console.ReadLine();

            Console.Write("Введите отчество (необязательно): ");
            note.Lastname = Console.ReadLine();

            Console.Write("Введите номер телефона (только цифры): ");
            note.PhoneNumber = Console.ReadLine();

            Console.Write("Введите страну: ");
            note.Country = Console.ReadLine();

            Console.Write("Введите дату рождения: ");
            note.Birthdate = Console.ReadLine();

            Console.Write("Введите организацию: ");
            note.Organization = Console.ReadLine();

            Console.Write("Введите должность: ");
            note.Position = Console.ReadLine();

            Console.Write("Введите прочую информацию: ");
            note.OtherNotes = Console.ReadLine();

            notes.Add(note);
            Console.Clear();
            Console.WriteLine("Поздравляю, запись успешно создана!");

        }

        private static void ShowAllNotes()
        {
            if (notes.Count == 0) {
                Console.WriteLine("На даный момент записей нет, но их же можно создать!");
                return;
            }
            foreach (var item in notes)
            {
                Console.WriteLine($"Имя: {item.Name}");
                Console.WriteLine($"Фамилия: {item.Surname}");
                Console.WriteLine($"Телефон: {item.PhoneNumber}");
                Console.WriteLine("****************************");
            }
        }

        private static void FindNote()
        {
            Console.WriteLine("По какому полю искать?");
            Console.WriteLine("нажмите 1 для поиска по имени");
            Console.WriteLine("нажмите 2 для поиска по фамилии");
            Console.WriteLine("нажмите 3 для поиска по телефону");
            var userInput = Console.ReadLine();
            var isItEnd = false;
            while (!isItEnd)
            {
                switch (userInput)
                {
                    case "1":
                    {
                        Console.WriteLine("Введите имя: ");
                        var input = Console.ReadLine();
                        while (input == "")
                        {
                            Console.WriteLine("Нельзя пустую стрроку, попробуй еще раз!");
                            Console.WriteLine("Введите имя: ");
                            input = Console.ReadLine();
                        }

                        foreach (var item in notes.Where(x => x.Name == input))
                        {
                            item.ShowAllInfo();
                        }
                    }

                        isItEnd = true;

                        break;
                    case "2":
                    {
                        Console.WriteLine("Введите фамилию: ");
                        var input = Console.ReadLine();
                        while (input == "")
                        {
                            Console.WriteLine("Нельзя пустую стрроку, попробуй еще раз!");
                            Console.WriteLine("Введите имя: ");
                            input = Console.ReadLine();
                        }

                        foreach (var item in notes.Where(x => x.Surname == input))
                        {
                            item.ShowAllInfo();
                        }
                    }

                        isItEnd = true;
                        break;
                    case "3":
                    {
                        Console.WriteLine("Введите телефон: ");
                        var input = Console.ReadLine();
                        while (input == "")
                        {
                            Console.WriteLine("Нельзя пустую стрроку, попробуй еще раз!");
                            Console.WriteLine("Введите имя: ");
                            input = Console.ReadLine();
                        }

                        foreach (var item in notes.Where(x => x.PhoneNumber == input))
                        {
                            item.ShowAllInfo();
                        }
                    }

                        isItEnd = true;
                        break;
                    default:
                        Console.WriteLine("Ничего не понимаю! Введи корректную команду");
                        break;
                }
            }
        }

        private static void EditNote()
        {
            FindNote();
            //TODO: подумать как вытащить номер эелемента и как оформить изменение полей
        }

        private static void DeleteNote()
        {
            FindNote();
            Console.Write("Введите id записи которую надо удалить: ");
            var id = Console.ReadLine();
            //TODO: подумать как вытащить номер эелемента
            //notes.Remove(notes.Where(x => x.Id.ToString() == id));
        }

        

    }
}