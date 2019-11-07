using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            Companion.PrintInfo();
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
                        Companion.NoteCreator();
                        break;
                    case "2":
                        Companion.Finder();
                        Companion.PrintInfo();
                        break;
                    case "3":
                        Companion.Editor();
                        break;
                    case "4":
                        Companion.Deleter();
                        break;
                    case "5":
                        ShowAllNotes();
                        break;
                    case "6":
                        Console.Clear();
                        Companion.PrintInfo();
                        break;
                    case "7":
                        Console.Clear();
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

        public static void CreateNote(Note note) => notes.Add(note);

        private static void ShowAllNotes()
        {
            Console.Clear();
            if (notes.Count == 0)
            {
                Console.WriteLine("На даный момент записей нет, но их же можно создать!\n");
                Companion.PrintInfo();
                return;
            }

            Console.WriteLine("Текущие записи: \n");
            foreach (var item in notes)
            {
                item.ShowMainInfo();
            }

            Console.WriteLine();
            Companion.PrintInfo();
        }

        public static List<int> FindNote(string searchParameter, string searchInfo)
        {
            var returnedId = new List<int>();
            switch (searchParameter)
            {
                case "Name":
                    foreach (var item in notes.Where(x => x.Name == searchInfo))
                    {
                        item.ShowAllInfo();
                        returnedId.Add(item.Id);
                    }

                    Console.WriteLine();
                    break;
                case "Surname":
                    foreach (var item in notes.Where(x => x.Surname == searchInfo))
                    {
                        item.ShowAllInfo();
                        returnedId.Add(item.Id);
                    }

                    Console.WriteLine();
                    break;
            }

            if (returnedId.Count != 0) return returnedId;
            Console.Clear();
            Console.WriteLine("Записей не найдено\n");
            return returnedId;
        }

        public static Note GetNoteToEdit(int id) => notes.First(x => x.Id == id);

        public static void DeleteNote(int[] id)
        {
            foreach (var item in id)
            {
                notes.Remove(notes.First(x => x.Id == item));
            }
        }
    }
}