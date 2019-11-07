using System;
using System.Collections.Generic;
using System.Text;

namespace NotebookApp
{
    static class Companion
    {
        public static void Greetings()
        {
            Console.WriteLine("Приветствую, это записная книжка, сейчас я покажу тебе что я умею!");
        }

        public static void PrintInfo()
        {
            Console.WriteLine("Для создания новой записи напиши \"1\"");
            Console.WriteLine("Для поиска записей напиши \"2\"");
            Console.WriteLine("Для редактирования записей напиши \"3\"");
            Console.WriteLine("Для удаления записи напиши \"4\"");
            Console.WriteLine("Для отображения всех записей напиши \"5\"");
            Console.WriteLine("Для показа справки напиши \"справка\"");
            Console.WriteLine("Для выхода из программы напиши \"пока\" (Помни, я всё забуду если закроюсь!)");
        }

        public static List<int> Finder()
        {
            Console.WriteLine("По какому полю искать?");
            Console.WriteLine("Введите 1 для поиска по имени");
            Console.WriteLine("Введите 2 для поиска по фамилии");

            var userInput = Console.ReadLine();
            while (true)
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
                        return Notebook.FindNote("Name", input);
                    }
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
                        return Notebook.FindNote("Surname", input);
                    }
                    default:
                        Console.WriteLine("Ничего не понимаю! Введи корректную команду");
                        break;
                }
            }
        }

        public static void Editor()
        {
            var x = Finder();
        }

        public static void Deleter()
        {
            var foundId = Finder();
            Console.WriteLine("Введите Id записи, которую нужно удалить, если хотите удалить всё, введите \"все\"");
            var userInput = Console.ReadLine();
            var id = -1;

            while (userInput == "все" || (int.TryParse(userInput, out id) && id > 0))
            {
                Console.WriteLine("Некорректный ввод!");
                Console.Write("Попробуй еще раз: ");
                userInput = Console.ReadLine();
            }

            Notebook.DeleteNote(id == -1 ? foundId.ToArray() : new [] {id});
        }

        public static void NoteCreator()
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

            Notebook.CreateNote(note);

            Console.Clear();
            Console.WriteLine("Поздравляю, запись успешно создана!");
        }
    }
}