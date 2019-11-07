using System;
using System.Collections.Generic;

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
            Console.WriteLine("Для показа справки напиши \"6\"");
            Console.WriteLine("Для выхода из программы напиши \"7\" (Помни, я всё забуду если закроюсь!)");
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
                            Console.WriteLine("Нельзя пустую строку, попробуй еще раз!");
                            Console.WriteLine("Введите имя: ");
                            input = Console.ReadLine();
                        }

                        Console.Clear();
                        Console.WriteLine("Найденные записи:\n");
                        return Notebook.FindNote("Name", input);
                    }
                    case "2":
                    {
                        Console.WriteLine("Введите фамилию: ");
                        var input = Console.ReadLine();
                        while (input == "")
                        {
                            Console.WriteLine("Нельзя пустую строку, попробуй еще раз!");
                            Console.WriteLine("Введите имя: ");
                            input = Console.ReadLine();
                        }

                        Console.Clear();
                        Console.WriteLine("Найденные записи:\n");
                        return Notebook.FindNote("Surname", input);
                    }
                    default:
                        Console.WriteLine("Ничего не понимаю! Введи корректную команду");
                        userInput = Console.ReadLine();
                        break;
                }
            }
        }

        public static void Editor()
        {
            var foundId = Finder();
            Console.WriteLine("Введите Id записи, которую надо редактировать");
            var userInput = Console.ReadLine();
            int id;

            while (!(int.TryParse(userInput, out id) && foundId.Contains(id)))
            {
                Console.WriteLine("Некорректный ввод!");
                Console.Write("Попробуй еще раз: ");
                userInput = Console.ReadLine();
            }

            var note = Notebook.GetNoteToEdit(id);
            string input;

            Console.WriteLine($"Текущая фамилия: {note.Surname}");
            Console.Write("Введите новую фамилию (чтобы оставить фамилию неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Surname = input;

            Console.WriteLine($"Текущее имя: {note.Name}");
            Console.Write("Введите новое имя (чтобы оставить имя неизменным, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Name = input;

            Console.WriteLine($"Текущее отчество: {note.Lastname}");
            Console.Write("Введите новое отчество (чтобы оставить отчество неизменным, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Lastname = input;

            Console.WriteLine($"Текущий телефон: {note.PhoneNumber}");
            Console.Write("Введите новый телефон (чтобы оставить телефон неизменным, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.PhoneNumber = input;

            Console.WriteLine($"Текущая страна: {note.Country}");
            Console.Write("Введите новую фамилию (чтобы оставить страну неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Country = input;

            Console.WriteLine($"Текущая дата рождения: {note.Birthdate}");
            Console.Write("Введите новую дату рождения (чтобы оставить страну неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Birthdate = input;

            Console.WriteLine($"Текущая организация: {note.Organization}");
            Console.Write("Введите новую организацию (чтобы оставить организацию неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Organization = input;

            Console.WriteLine($"Текущая должность: {note.Position}");
            Console.Write("Введите новую должность (чтобы оставить должность неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.Position = input;

            Console.WriteLine($"Текущая заметка: {note.OtherNotes}");
            Console.Write("Введите новую заметку (чтобы оставить заметку неизменной, введите \"-\"): ");
            input = Console.ReadLine();
            if (input != "-") note.OtherNotes = input;

            Console.Clear();
            Console.WriteLine("Поздравляю, запись успешно обновлена!\n");
            PrintInfo();
        }

        public static void Deleter()
        {
            var foundId = Finder();
            if (foundId.Count != 0)
            {
                Console.WriteLine("Введите Id записи, которую нужно удалить, если хотите удалить всё, введите \"все\"");
                var userInput = Console.ReadLine();
                var id = -1;

                while (!(userInput == "все" || int.TryParse(userInput, out id) && foundId.Contains(id)))
                {
                    Console.WriteLine("Некорректный ввод!");
                    Console.Write("Попробуй еще раз: ");
                    userInput = Console.ReadLine();
                }


                Notebook.DeleteNote(id == -1 ? foundId.ToArray() : new[] {id});

                Console.Clear();
                Console.WriteLine("Выбранное успешно удалено\n");
            }

            PrintInfo();
        }

        public static void NoteCreator()
        {
            var note = new Note();
            Console.Clear();

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

            Console.Write("Введите дату рождения (необязательно): ");
            note.Birthdate = Console.ReadLine();

            Console.Write("Введите организацию (необязательно): ");
            note.Organization = Console.ReadLine();

            Console.Write("Введите должность (необязательно): ");
            note.Position = Console.ReadLine();

            Console.Write("Введите прочую информацию (необязательно): ");
            note.OtherNotes = Console.ReadLine();

            Notebook.CreateNote(note);

            Console.Clear();
            Console.WriteLine("Поздравляю, запись успешно создана!\n");
            PrintInfo();
        }
    }
}