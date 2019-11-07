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
                        break;
                    case "3":
                        EditNote();
                        break;
                    case "4":
                        //DeleteNote();
                        break;
                    case "5":
                        ShowAllNotes();
                        break;
                    case "6":
                        Companion.PrintInfo();
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

        public static void CreateNote(Note note) => notes.Add(note);

        private static void ShowAllNotes()
        {
            if (notes.Count == 0) {
                Console.WriteLine("На даный момент записей нет, но их же можно создать!");
                return;
            }
            foreach (var item in notes)
            {
                item.ShowMainInfo();
            }
        }

        public static List<int> FindNote(string searchParameter, string searchInfo)
        {
            var returnedId = new List<int>();
            switch (searchParameter)
            {
                case "Name":
                    foreach (var item in notes.Where(x=> x.Name == searchInfo))
                    {
                        item.ShowAllInfo();
                        returnedId.Add(item.Id);

                    }
                    break;
                case "Surname":
                    foreach (var item in notes.Where(x => x.Surname == searchInfo))
                    {
                        item.ShowAllInfo();
                        returnedId.Add(item.Id);
                    }
                    break;
            }
            return returnedId;
        }

        private static void EditNote()
        {
            
            //TODO: подумать как вытащить номер элемента и как оформить изменение полей
        }

        public static void DeleteNote(int[] id)
        {
            foreach (var item in notes)
            {
                
            }
            
        }

        

    }
}