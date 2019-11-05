using System;
using System.Collections.Generic;
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
            var isItEnd = true;
            while (isItEnd)
            {
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        CreateNote();
                        break;
                    case "2":
                        Console.WriteLine("поиск");
                        break;
                    case "3":
                        Console.WriteLine("ред");
                        break;
                    case "4":
                        Console.WriteLine("удаление");
                        break;
                    case "5":
                        ShowAllNotes();
                        break;
                    case "справка":
                        Console.WriteLine("справка");
                        break;
                    case "пока":
                        Console.WriteLine("Прощай, друг");
                        Thread.Sleep(1000);
                        isItEnd = false;
                        break;
                    default:
                        Console.WriteLine("Ничего не понимаю! Введи корректную команду");
                        break;
                }
                
            }
        }

        static void CreateNote()
        {
        }

        static void ShowAllNotes()
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
                Console.WriteLine($"");
            }
        }

        static void FindNote()
        {
            throw new NotImplementedException();
        }

        static void EditNote()
        {
            throw new NotImplementedException();
        }

        static void DeleteNote()
        {
            throw new NotImplementedException();
        }

    }
}